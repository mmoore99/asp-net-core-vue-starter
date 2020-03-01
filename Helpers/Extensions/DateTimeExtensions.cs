using System;
using System.Globalization;
using TimeZoneConverter;

namespace Fbits.VueMpaTemplate.Helpers.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsInDateRange(this DateTime date, DateTime startDate, DateTime endDate)
        {
            return date.Date >= startDate.Date && date.Date <= endDate.Date;
        }

        public static bool IsInTimeRange(this DateTime dateTime, DateTime startTime, DateTime endTime)
        {
            return dateTime >= startTime && dateTime <= endTime;
        }

        public static DateTime Est(this DateTime dateTime)
        {
            return InTimeZone(dateTime, "Eastern Standard Time");
        }

        public static DateTime Cst(this DateTime dateTime)
        {
            return InTimeZone(dateTime, "Central Standard Time");
        }

        public static DateTime Mst(this DateTime dateTime)
        {
            return InTimeZone(dateTime, "Mountain Standard Time");
        }

        public static DateTime Pst(this DateTime dateTime)
        {
            return InTimeZone(dateTime, "Pacific Standard Time");
        }

        public static DateTime InTimeZone(this DateTime dateTime, string timeZoneId)
        {
            return TimeZoneInfo.ConvertTime(dateTime, TZConvert.GetTimeZoneInfo(timeZoneId));
        }

        public static DateTimeOffset Cst(this DateTimeOffset dateTime)
        {
            return InTimeZone(dateTime, "Central Standard Time");
        }

        public static DateTimeOffset Mst(this DateTimeOffset dateTime)
        {
            return InTimeZone(dateTime, "Mountain Standard Time");
        }

        public static DateTimeOffset Pst(this DateTimeOffset dateTime)
        {
            return InTimeZone(dateTime, "Pacific Standard Time");
        }

        public static DateTimeOffset InTimeZone(this DateTimeOffset dateTime, string timeZoneId)
        {
            return TimeZoneInfo.ConvertTime(dateTime, TZConvert.GetTimeZoneInfo(timeZoneId));
        }

        public static DateTimeOffset? Cst(this DateTimeOffset? dateTime)
        {
            return InTimeZone(dateTime, "Central Standard Time");
        }

        public static DateTimeOffset? Mst(this DateTimeOffset? dateTime)
        {
            return InTimeZone(dateTime, "Mountain Standard Time");
        }

        public static DateTimeOffset? Pst(this DateTimeOffset? dateTime)
        {
            return InTimeZone(dateTime, "Pacific Standard Time");
        }

        public static DateTimeOffset? InTimeZone(this DateTimeOffset? dateTime, string timeZoneId)
        {
            return TimeZoneInfo.ConvertTime((DateTimeOffset) dateTime, TZConvert.GetTimeZoneInfo(timeZoneId));
        }

        public static int DaysInMonth(this DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }

        public static bool IsWeekday(this DateTime date)
        {
            return !date.ToString("ddd").StartsWith("S");
        }

        public static bool IsWeekend(this DateTime date)
        {
            return date.ToString("ddd").StartsWith("S");
        }

        public static int DayOfWeekIndex(this DateTime date)
        {
            return (int) CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
        }

        public static DateTime ConvertToDateTime(this DateTimeOffset dateTime)
        {
            if (dateTime.Offset.Equals(TimeSpan.Zero))
                return dateTime.UtcDateTime;
            else if (dateTime.Offset.Equals(TimeZoneInfo.Local.GetUtcOffset(dateTime.DateTime)))
                return DateTime.SpecifyKind(dateTime.DateTime, DateTimeKind.Local);
            else
                return dateTime.DateTime;
        }
    }
}
