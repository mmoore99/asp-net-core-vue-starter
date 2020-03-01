using System;
using TimeZoneConverter;

namespace Fbits.VueMpaTemplate.Helpers.DateAndTime
{
    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;

        public static DateTime UtcNow => TimeZoneInfo.ConvertTimeBySystemTimeZoneId(Now(), TimeZoneInfo.Local.Id, TimeZoneInfo.Utc.Id);

        public static DateTime CentralNow => TimeZoneInfo.ConvertTime(Now(), TimeZoneInfo.Local, TZConvert.GetTimeZoneInfo("Central Standard Time"));

        public static DateTime PacificNow => TimeZoneInfo.ConvertTime(Now(), TimeZoneInfo.Local, TZConvert.GetTimeZoneInfo("Pacific Standard Time"));
    }
}