using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Fbits.VueMpaTemplate.Helpers.Extensions
{
    public static class DynamicExtensions
    {
        public static dynamic ToDynamic(this object anonymousObject)
        {
            var x = new ExpandoObject();
            var values =
                from property in anonymousObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                select new {
                               property.Name,
                               Value = property.GetValue(anonymousObject, null)
                           };
            foreach (var property in values) (x as IDictionary<string, object>).Add(property.Name, property.Value);
            return x;
        }

        public static dynamic ToExpando(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType())) expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }
    }
}
