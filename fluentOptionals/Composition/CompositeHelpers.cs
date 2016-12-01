using System;

namespace FluentOptionals.Composition
{
    internal static class CompositeHelpers
    {
        private static readonly Func<bool> False = () => false;
        
        public static bool IsSome<T>(this Optional<T> optional)
        {
            return optional.Match(_ => true, False);
        }

        public static T ValueOrDefault<T>(this Optional<T> optional)
        {
            return optional.ValueOr(default(T));
        }
    }
}