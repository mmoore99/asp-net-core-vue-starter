using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Fbits.VueMpaTemplate.Helpers.Extensions
{
    public static class QueryStringExtensions
    {
        public static string ToQueryString(this NameValueCollection parameters)
        {
            var items = parameters.AllKeys
            .SelectMany(parameters.GetValues, (k, v) => k + "=" + HttpUtility.UrlEncode(v))
            .ToArray();
            return String.Join("&", items);
        }

        public static string ToQueryString2(this NameValueCollection parameters, Boolean omitEmpty = true)
        {
            var items = new List<String>();
            for (var i = 0; i < parameters.Count; i++)
            {
                foreach (var value in parameters.GetValues(i))
                {
                    var addValue = (omitEmpty) ? !String.IsNullOrEmpty(value) : true;
                    if (addValue) items.Add(String.Concat(parameters.GetKey(i), "=", HttpUtility.UrlEncode(value)));
                }
            }
            return String.Join("&", items.ToArray());
        }
    }
}
