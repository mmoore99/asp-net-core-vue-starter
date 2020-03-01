using System;

namespace Fbits.VueMpaTemplate.Helpers.DateAndTime
{
    public class DateUtils
    {
         public static DateTime NextTimeAtMinutesAfterHour(int minutes)
         {
             var result = DateTime.Today.Add(new TimeSpan(DateTime.Now.Hour, minutes, 0));
             if (minutes <= DateTime.Now.Minute) result = result.AddHours(1);
             return result;
         }
    }
}