using Fbits.VueMpaTemplate.Helpers.Extensions;
using Fbits.VueMpaTemplate.Helpers.Nlog.CustomLayoutRenderers;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;

namespace Fbits.VueMpaTemplate.Configuration
{
    public class NlogConfig
    {
        public void CreateConfig(string fileName, bool isEnableInternalLog = false)
        {
            const string rendererName = "timeStamp";
            if (!ConfigurationItemFactory.Default.LayoutRenderers.TryGetDefinition(rendererName, out var temp))
            {
                ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition(rendererName, typeof(TimeStampRenderer));
            }

            var config = new LoggingConfiguration();

            if (isEnableInternalLog)
            {
                LogManager.ThrowExceptions = true;
                InternalLogger.LogFile = @"c:\temp\NlogLog.txt";
                InternalLogger.LogLevel = LogLevel.Debug;
                InternalLogger.LogToConsole = true;
            }

            var fileTarget = CreateFileTarget(fileName);
            config.AddTarget("File", fileTarget);
            //config.LoggingRules.Add(new LoggingRule("Fiddle*", LogLevel.Info, fileTarget));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, fileTarget));

            var consoleTarget = CreateConsoleTarget();
            config.AddTarget("Console", consoleTarget);
            //config.LoggingRules.Add(new LoggingRule("Fiddle*", LogLevel.Info, consoleTarget));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, consoleTarget));

            LogManager.Configuration = config;
        }

        private static FileTarget CreateFileTarget(string fileName)
        {
            return new FileTarget
            {
                Name = "File",
                FileName = fileName,
                //Layout = @"${date:UniversalTime=true :format=MM/dd/yy  HH\:mm\:ss }|${level}|${logger:shortName=true}|${message}"
                //Layout = @"${date:UniversalTime=true :format=MM/dd/yy  HH\:mm\:ss }|${level:padding=-5:padCharacter= }|${logger:shortName=true:padding=-20:padCharacter= }|${message}"
                Layout = @"${timeStamp:TimeZone=pst:format=MM/dd/yy  HH\:mm\:ss}|${level:padding=-5:padCharacter= }|${logger:shortName=true:padding=-20:padCharacter= }|${message}",
                ArchiveFileName = "log.{#}.txt",
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.Date,
                ArchiveDateFormat = "yyyyMMdd"
            };
        }

        private static ConsoleTarget CreateConsoleTarget()
        {
            return new ConsoleTarget
            {
                Layout = @"${timeStamp:TimeZone=pst:format=MM/dd/yy  HH\:mm\:ss}|${level:padding=-5:padCharacter= }|${logger:shortName=true:padding=-20:padCharacter= }|${message}",
            };
        }

        public void ModifyConfig(string fileName, bool isEnableInternalLog = false)
        {
            var config = LogManager.Configuration;
            var target = (FileTarget)config.FindTargetByName("File");
            target.FileName = "Log.txt";
            LogManager.Configuration = config;
            LogManager.ReconfigExistingLoggers();
        }
    }
}