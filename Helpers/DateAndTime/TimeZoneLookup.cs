using System.Collections.Generic;

namespace Fbits.VueMpaTemplate.Helpers.DateAndTime
{
    public class TimeZoneLookup
    {
        private Dictionary<string, string> TimeZoneLookupTable = new Dictionary<string, string> {
            {"pst", "Pacific Standard Time"},
            {"cst", "Central Standard Time"},
            {"mst", "Mountain Standard Time"},
            {"est", "Mountain Standard Time"},
            {"pdt", "Pacific Standard Time"},
            {"cdt", "Central Standard Time"},
            {"mdt", "Mountain Standard Time"},
            {"edt", "Mountain Standard Time"},
        };

        public string Lookup(string timezoneAbbreviation)
        {
            return TimeZoneLookupTable[timezoneAbbreviation.ToLower().Trim()] ?? "Invalid Time Zone Abbreviation";
        }
    }
}