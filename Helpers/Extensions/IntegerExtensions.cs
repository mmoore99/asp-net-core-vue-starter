namespace Fbits.VueMpaTemplate.Helpers.Extensions
{
    public static class IntegerExtensions
    {
        public static bool IsEven(this int value)
        {
            return (value & 1) == 0;
        }

        public static bool IsOdd(this int value)
        {
            return !value.IsEven();
        }
    }
}