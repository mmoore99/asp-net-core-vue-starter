using System;

//namespace FiddleFlights.Core.Helpers.Extensions
//{
    namespace Fbits.VueMpaTemplate.Helpers.Extensions
    {
        public static class ReflectionExtensions
        {
            public static string GetFullyQualifiedTypeNameWithAssembly(this Type type)
            {
                return type.FullName + ", " + type.Assembly.FullName;
            }


            public static object CreateInstance(this string fullTypeNameWithAssembly, params object[] constructorArgs)
            {
                return Activator.CreateInstance(Type.GetType(fullTypeNameWithAssembly), constructorArgs);
            }



        }
    }
//}