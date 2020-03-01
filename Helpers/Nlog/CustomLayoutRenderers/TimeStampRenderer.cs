using System.Globalization;
using System.Text;
using Fbits.VueMpaTemplate.Helpers.DateAndTime;
using Fbits.VueMpaTemplate.Helpers.Extensions;
using NLog;
using NLog.Config;
using NLog.LayoutRenderers;

namespace Fbits.VueMpaTemplate.Helpers.Nlog.CustomLayoutRenderers
{
    [LayoutRenderer("timeStamp")]
    public class TimeStampRenderer : LayoutRenderer
    {
        private const string DefaultTimeZone = "UTC";
        private const string DefaultTimeZoneId = "UTC";

        public TimeStampRenderer()
        {
            TimeZone = DefaultTimeZone;
            IsShowTimeZone = false;
        }

        public CultureInfo Culture { get; set; }

        [DefaultParameter]
        public string Format { get; set; }
        
        public string TimeZone { get; set; }
        
        public bool IsShowTimeZone { get; set; }

        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            string timeZoneId;
            switch (TimeZone.ToLower())
            {
                case "pst":
                    timeZoneId = "Pacific Standard Time";
                    break;
                case "pdt":
                    timeZoneId = "Pacific Standard Time";
                    break;
                case "mst":
                    timeZoneId = "Mountain Standard Time";
                    break;
                case "mdt":
                    timeZoneId = "Mountain Standard Time";
                    break;
                case "cst":
                    timeZoneId = "Central Standard Time";
                    break;
                case "cdt":
                    timeZoneId = "Central Standard Time";
                    break;
                case "est":
                    timeZoneId = "Eastern Standard Time";
                    break;
                case "edt":
                    timeZoneId = "Eastern Standard Time";
                    break;
                case "utc":
                    timeZoneId = "UTC";
                    break;
                default:
                    timeZoneId = DefaultTimeZoneId;
                    break;
            }
            builder.Append($"{SystemTime.Now().InTimeZone(timeZoneId).ToString(Format, Culture)} {((IsShowTimeZone) ? TimeZone.ToUpper() : string.Empty)}");
        }
    }
}
