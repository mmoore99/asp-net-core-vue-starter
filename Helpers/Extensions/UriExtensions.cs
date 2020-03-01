using System;
using System.Linq;

namespace Fbits.VueMpaTemplate.Helpers.Extensions
{
    public static class UriExtensions
    {
        public static Uri Append(this Uri uri, params string[] paths)
        {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => $"{current.TrimEnd('/')}/{path.TrimStart('/')}"));
        }
    }

    public static class UriUtils
    {
        public static Uri Combine(Uri baseUrl, params string[] paths)
        {
            return baseUrl.Append(paths);
        }

        public static Uri Combine(string baseUrl, params string[] paths)
        {
            return new Uri(baseUrl).Append(paths);
        }

        public static string CombineAsString(string baseUrl, params string[] paths)
        {
            return new Uri(baseUrl).Append(paths).ToString();
        }


    }
}