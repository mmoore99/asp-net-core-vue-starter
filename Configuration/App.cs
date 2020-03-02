using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using LoggingExtensions.Logging;
using LoggingExtensions.NLog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Fbits.VueMpaTemplate.Configuration
{
    public static class App
    {
        public const string DEVELOPMENT_BUILD_CONFIGURATION = "development";
        public const string PRODUCTION_BUILD_CONFIGURATION = "production";
        public const string STAGING_BUILD_CONFIGURATION = "staging";
        public const string LINUX_BUILD_CONFIGURATION = "linux";
        public const string APPSETTINGS_SECTION_KEY = "AppSettings";
        public const string CONNECTION_STRINGS_SECTION_KEY = "ConnectionStrings";
        public const string NLOG_FILE_LOCATION = "Log.txt";
        public const string FILE_STORAGE_PATH = "App_Data/Files/";
        public const string LOG_DIVIDER = "===========================================================================================================================";

        private static readonly string _this;
        public static string DotnetCoreRuntime = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;

        static App()
        {
            _this = typeof(App).FullName;
        }

        public static IConfiguration Config { get; set; }
        public static IServiceCollection Services { get; set; }
        public static string BuildConfiguration { get; set; }
        public static string ApplicationBaseDirectory { get; set; }
        public static string Version { get; set; }
        public static string WwwRootPath { get; set; }
        public static string ContentRootPath { get; set; }
        public static IConfigurationSection ConfigConnectionStrings { get; set; }
        public static IConfigurationSection ConfigAppSettings { get; set; }
        public static string FileStoragePhysicalPath { get; set; }
        public static bool IsNlogInitialized { get; set; }
        public static ServiceProvider ServiceLocator { get; set; }
        public static List<string> SettingsFileNames { get; private set; }
        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        public static string OSDescription => RuntimeInformation.OSDescription;
        public static string FrameworkDescription => RuntimeInformation.FrameworkDescription;

        public static void OnApplicationStart(IWebHostEnvironment env, IConfiguration configuration)
        {
            Version = PlatformServices.Default.Application.ApplicationVersion;
            ContentRootPath = env.ContentRootPath;
            WwwRootPath = env.WebRootPath;
            ApplicationBaseDirectory = Directory.GetCurrentDirectory();
            BuildConfiguration = SetBuildConfiguration();
            Config = configuration;
            ConfigAppSettings = Config.GetSection(APPSETTINGS_SECTION_KEY);
            ConfigConnectionStrings = Config.GetSection(CONNECTION_STRINGS_SECTION_KEY);
            InitializeNlog(NLOG_FILE_LOCATION, isEnableInternalLog: false);
            MapPhysicalPaths();
            CreateDirectories();
            LogSettings();
        }

        public static string SetBuildConfiguration()
        {
            // following allows inclusion of code with preprocessor directives /
            // /see https://github.com/dotnet/templating/issues/1354
            //-:cnd:noEmit
            #if DEBUG
            return DEVELOPMENT_BUILD_CONFIGURATION;
            #elif STAGE
                        return STAGING_BUILD_CONFIGURATION;
            #elif RELEASE
                        return PRODUCTION_BUILD_CONFIGURATION;
            #elif LINUX
                        return LINUX_BUILD_CONFIGURATION;
            #endif
            //+:cnd:noEmit
        }

        public static void InitializeNlog(string logFileName, bool isEnableInternalLog = false)
        {
            new NlogConfig().CreateConfig(logFileName, isEnableInternalLog);
            Log.InitializeWith<NLogLog>();
            IsNlogInitialized = true;
        }

        public static void MapPhysicalPaths()
        {
            FileStoragePhysicalPath = MapWwwPath(FILE_STORAGE_PATH);
        }

        public static string MapWwwPath(string path)
        {
            return Path.Combine(WwwRootPath, path);
        }

        public static string MapContentPath(string path)
        {
            return Path.Combine(ContentRootPath, path);
        }

        private static void CreateDirectories()
        {
            Directory.CreateDirectory(FileStoragePhysicalPath);
        }

        private static void LogSettings()
        {
            _this.Log().Info(LOG_DIVIDER);
            _this.Log().Info(LOG_DIVIDER);
            _this.Log().Info($"Application Started: Version = {Version}");
            _this.Log().Info($"Operating System = {OSDescription}");
            _this.Log().Info($"DotnetCore Runtime = {DotnetCoreRuntime}");
            _this.Log().Info($"Framework = {FrameworkDescription}");
            _this.Log().Info($"Build Configuration = {BuildConfiguration}");
            LogAllConfigSections(_this);
            _this.Log().Info(LOG_DIVIDER);
            LogSelectedSettings();
            _this.Log().Info(LOG_DIVIDER);
            LogPhysicalFileLocations();
            _this.Log().Info(LOG_DIVIDER);
        }

        public static void LogSelectedSettings()
        {
            _this.Log().Info("Selected Settings:");
            //_this.Log().Info($"    Settings File(s) = {SettingsFileNames.Join(", ")}");
            //_this.Log().Info($"    RavenDB Connection String = {RavenDbConnectionString}");
            //_this.Log().Info($"    RavenDB Certificate Path = {RavenDbCertPhysicalPath}");
            //_this.Log().Info($"    Logentries Token = {LogentriesToken}");
            //_this.Log().Info($"    Base Url = {FiddleMonGlobals.WebsiteUrl}");
        }

        private static void LogPhysicalFileLocations()
        {
            _this.Log().Info($"Physical File Locations:");
            _this.Log().Info($"  ContentRootPath:                          {ContentRootPath}");
            _this.Log().Info($"  WwwRootPath:                              {WwwRootPath}");
            _this.Log().Info($"  FileStoragePhysicalPath:                  {FileStoragePhysicalPath}");
        }

        public static void LogAppSettings()
        {
            _this.Log().Info("All AppSettings:");
            LogKeyValuePairCollection(ConfigAppSettings.AsEnumerable());
        }

        public static void LogConnectionStrings()
        {
            _this.Log().Info("All Connection Strings:");
            LogKeyValuePairCollection(ConfigConnectionStrings.AsEnumerable());
        }

        public static void LogAllConfigSections(string className)
        {
            _this.Log().Info(LOG_DIVIDER);
            _this.Log().Info("All Config Sections:");
            foreach (var configurationSection in Config.GetChildren())
            {
                LogConfigurationSection(configurationSection);
                _this.Log().Info(string.Empty);
            }
        }

        private static void LogConfigurationSection(IConfigurationSection configurationSection)
        {
            _this.Log().Info($"Config Section: {configurationSection.Key}");
            LogKeyValuePairCollection(configurationSection.AsEnumerable());
        }

        private static void LogKeyValuePairCollection(IEnumerable<KeyValuePair<string, string>> keyValuePaircollection)
        {
            foreach (var keyValuePair in keyValuePaircollection.Where(kvp => kvp.Value != null))
            {
                _this.Log().Info($"    {keyValuePair.Key}={keyValuePair.Value}");
            }
        }
    }
}