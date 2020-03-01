using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Fbits.VueMpaTemplate.Helpers
{
    public static class Utilities
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethodName(bool isShortName = false)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);
            var declaringClass = sf.GetMethod().DeclaringType.Name;
            if (declaringClass.IndexOf('`') > 0) declaringClass = declaringClass.Substring(0, declaringClass.IndexOf('`'));
            return isShortName ? sf.GetMethod().Name : declaringClass + "." + sf.GetMethod().Name;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentClassName()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);
            var declaringClass = sf.GetMethod().DeclaringType.Name;
            if (declaringClass.IndexOf('`') > 0) declaringClass = declaringClass.Substring(0, declaringClass.IndexOf('`'));
            return declaringClass;
        }


        public static List<string> ReadFileToList(string pFilePath)
        {
            var result = new List<string>();
            try
            {
                using (var sr = new StreamReader(pFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null) result.Add(line);
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public static long GetCurrentTimeInEpochSeconds()
        {
            var epochTicks = (DateTime.Now.ToUniversalTime().AddSeconds(3).Ticks - 621355968000000000);
            return epochTicks / 10000000;
        }

        public static bool IsDebugMode()
        {
            var debugging = false;

            WellAreWe(ref debugging);

            return debugging;
        }

        [Conditional("DEBUG")]
        private static void WellAreWe(ref bool debugging)
        {
            debugging = true;
        }


    }


}
