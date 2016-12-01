using System;

namespace FluentOptionals
{
    public static class OptionalCreateExtensions
    {
        public static Optional<T> ToSome<T>(this T value)
        {
            return Optional.Some(value);
        }

        // ReSharper disable once UnusedParameter.Global
        public static Optional<T> ToNone<T>(this T value)
        {
            return Optional.None<T>();
        }


        public static Optional<T> ToOptional<T>(this T value)
        {
            return Optional.From(value);
        }

        public static Optional<T> ToOptional<T>(this T value, Func<T, bool> condition)
        {
            return Optional.From(value, condition);
        }
    }
}