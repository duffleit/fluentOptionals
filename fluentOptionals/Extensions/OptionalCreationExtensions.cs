using System;

namespace fluentOptionals.Extensions
{
    public static class OptionalCreationExtensions
    {
        public static Optional<T> ToSome<T>(this T value)
        {
            return Optional.Some(value);
        }

        public static Optional<T> ToSomeWhen<T>(this T value, Predicate<T> condition)
        {
            return condition(value) ? Optional.Some(value) : Optional.None<T>();
        }

        public static Optional<T> ToSomeWhenNotNull<T>(this T value)
        {
            return (value == null) ? Optional.None<T>() : Optional.From(value);
        }
    }
}
