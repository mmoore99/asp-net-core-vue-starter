using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Fbits.VueMpaTemplate.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsAnyOf(this string sourceString, IEnumerable<string> possibleMatches)
        {
            return possibleMatches.Any(s => sourceString.Contains(s, StringComparison.OrdinalIgnoreCase));
        }

        public static bool DoesNotContainAnyOf(this string sourceString, IEnumerable<string> possibleMatches)
        {
            return !ContainsAnyOf(sourceString, possibleMatches);
        }

        public static int TryToParseInt(this string pString)
        {
            if (pString == null) return -1;
            int result;
            return Int32.TryParse(pString, out result) ? result : -1;
        }

        public static string TryToGetValue(this string pString)
        {
            if (pString == null) return "null";
            return pString;
        }

        public static T TryToParseEnum<T>(this string pString)
        {
            if (Enum.IsDefined(typeof(T), pString)) return (T)Enum.Parse(typeof(T), pString, true);
            else
            {
                foreach (var value in Enum.GetNames(typeof(T))) if (value.Equals(pString, StringComparison.OrdinalIgnoreCase)) return (T)Enum.Parse(typeof(T), value);
                return default(T);
            }
        }

        public static string Truncate(this string pString, int pMaxLength)
        {
            return pString.Substring(0, Math.Min(pString.Length, pMaxLength));
        }

        public static string AppendStartAndEndSlashesIfNeeded(this string pString)
        {
            return string.Format("{0}{1}{2}",
                                 (pString.StartsWith("/") ? string.Empty : "/"),
                                 pString,
                                 pString.EndsWith("/") ? string.Empty : "/");
        }

        public static string CombineToPath(this string path, string root)
        {
            if (Path.IsPathRooted(path)) return path;

            return Path.Combine(root, path);
        }

        public static void IfNotNull(this string target, Action<string> continuation)
        {
            if (target != null) continuation(target);
        }

        public static string ToFullPath(this string path)
        {
            return Path.GetFullPath(path);
        }

        public static string ParentDirectory(this string path)
        {
            return Path.GetDirectoryName(path);
        }

        public static bool IsEmpty(this string stringValue)
        {
            return string.IsNullOrEmpty(stringValue);
        }

        public static bool IsNotEmpty(this string stringValue)
        {
            return !string.IsNullOrEmpty(stringValue);
        }

        public static void IsNotEmpty(this string stringValue, Action<string> action)
        {
            if (stringValue.IsNotEmpty()) action(stringValue);
        }

        public static bool ToBool(this string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue)) return false;

            return bool.Parse(stringValue);
        }

        public static string ToFormat(this string stringFormat, params object[] args)
        {
            return String.Format(stringFormat, args);
        }

        public static bool EqualsIgnoreCase(this string thisString, string otherString)
        {
            return thisString.Equals(otherString, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string Capitalize(this string stringValue)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stringValue);
        }

        public static string ConvertCRLFToBreaks(this string plainText)
        {
            return new Regex("(\r\n|\n)").Replace(plainText, "<br/>");
        }

        public static DateTime ToDateTime(this string dateTimeValue)
        {
            return DateTime.Parse(dateTimeValue);
        }

        public static DateTime ToDateTimeSafe(this string dateTimeString)
        {
            return dateTimeString.ToDateTimeSafe(new DateTime());
        }

        public static DateTime ToDateTimeSafe(this string dateTimeString, DateTime defaultValue)
        {
            DateTime dateTimeResult;
            return DateTime.TryParse(dateTimeString, out dateTimeResult) ? dateTimeResult : defaultValue;
        }

        public static string ToGmtFormattedDate(this DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd hh':'mm':'ss tt 'GMT'");
        }

        public static string[] ToDelimitedArray(this string content)
        {
            return content.ToDelimitedArray(',');
        }

        public static string[] ToDelimitedArray(this string content, char delimiter)
        {
            var array = content.Split(delimiter);
            for (var i = 0; i < array.Length; i++) array[i] = array[i].Trim();

            return array;
        }

        public static bool IsValidNumber(this string number)
        {
            return IsValidNumber(number, Thread.CurrentThread.CurrentCulture);
        }

        public static bool IsValidNumber(this string number, CultureInfo culture)
        {
            var _validNumberPattern =
                @"^-?(?:\d+|\d{1,3}(?:"
                + culture.NumberFormat.NumberGroupSeparator +
                @"\d{3})+)?(?:\"
                + culture.NumberFormat.NumberDecimalSeparator +
                @"\d+)?$";

            return new Regex(_validNumberPattern, RegexOptions.ECMAScript).IsMatch(number);
        }

        public static IList<string> getPathParts(this string path)
        {
            return path.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static string DirectoryPath(this string path)
        {
            return Path.GetDirectoryName(path);
        }

        public static IEnumerable<string> ReadLines(this string text)
        {
            var reader = new StringReader(text);
            string line;
            while ((line = reader.ReadLine()) != null) yield return line;
        }

        public static void ReadLines(this string text, Action<string> callback)
        {
            var reader = new StringReader(text);
            string line;
            while ((line = reader.ReadLine()) != null) callback(line);
        }


        public static string SubstringUntilFirst(this string source, string matchString)
        {
            if (source.IsEmpty()) return string.Empty;
            var index = source.IndexOf(matchString, StringComparison.CurrentCultureIgnoreCase);
            return (index < 0) ? source : source.Substring(0, index);
        }

        public static string SubstringThruFirst(this string source, string matchString)
        {
            if (source.IsEmpty()) return string.Empty;
            var index = source.IndexOf(matchString, StringComparison.CurrentCultureIgnoreCase);
            return (index < 0) ? source : source.Substring(0, index + matchString.Length);
        }

        public static string SubstringAfterLast(this string source, string matchString)
        {
            if (source.IsEmpty()) return string.Empty;
            var index = source.IndexOf(matchString, StringComparison.CurrentCultureIgnoreCase);
            return (index < 0) ? source : source.Substring(index + 1);
        }

        public static string ExtractViewNameWithoutExtensions(this string source)
        {
            return Regex.Match(source, @".+/(?<Name>.+?)\.").Groups["Name"].Value;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        private static readonly Regex MultipleSpaces = new Regex(@" {2,}", RegexOptions.Compiled);

        public static string RemoveDoubleSpaces(this string input)
        {
            return MultipleSpaces.Replace(input, " ");
        }

        private static readonly Regex CRLF = new Regex("\\n|\\t", RegexOptions.Compiled);

        public static string RemoveCRLF(this string input)
        {
            return CRLF.Replace(input, "");
        }

        public static string ConcatWithSeperator(this IEnumerable<string> pStrings, string pSeperator)
        {
            var list = pStrings.ToList();
            var result = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                result.Append(list[i]);
                if (i != list.Count - 1) result.Append(pSeperator);
            }
            return result.ToString();
        }

        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            var firstChar = value[0];
            if (char.IsLower(firstChar))
            {
                return value;
            }
            firstChar = char.ToLowerInvariant(firstChar);
            return firstChar + value.Substring(1);
        }

        public static string FirstLetterToUpper(this string value)
        {
            if (value == null) return null;
            if (value.Length > 1) return char.ToUpper(value[0]) + value.Substring(1);
            return value.ToUpper();
        }

        public static char[] FirstChars(this string value)
        {
            return value.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries).Select(s => s[0]).ToArray();
        }

        public static string[] Words(this string value)
        {
            return value.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ToMyTitleCase(this string value)
        {
            value = value.ToLower();
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(value); //War And Peace
        }

        public static string StripController(this string source)
        {
            return source.Replace("Controller", "");
        }

        public static string ExtractDigits(this string source)
        {
            return Regex.Replace(source, "[^0-9]", "");
        }

    }


}