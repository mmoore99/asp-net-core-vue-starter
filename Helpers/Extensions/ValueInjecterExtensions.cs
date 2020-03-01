using System.Collections.Generic;
using Omu.ValueInjecter;

namespace Fbits.VueMpaTemplate.Helpers.Extensions
{
    public static class ValueInjecterExtensions
    {
        public static ICollection<TTo> InjectFrom<TFrom, TTo>(this ICollection<TTo> to, IEnumerable<TFrom> from) where TTo : new()
        {
            foreach (var source in from)
            {
                to.Add((TTo) new TTo().InjectFrom(source));
            }
            return to;
        }
    }
}