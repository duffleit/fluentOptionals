using System;

namespace NOptional
{
    public static class OptionalCreationExtensions
    {
        public static Optional<T> SomeIfNotNull<T>(this T value)
        {
            return value == null ? Optional.None<T>() : Optional.Some(value);
        }

        public static Optional<T> SomeIf<T>(this T value, Predicate<T> condition)
        {
            return condition(value) ? Optional.Some(value) : Optional.None<T>();
        } 
    }
}
