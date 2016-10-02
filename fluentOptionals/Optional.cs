using System;

namespace fluentOptionals
{
    public static class Optional
    {
        public static Optional<T> None<T>()
        {
            return new Optional<T>();
        }

        public static Optional<T> Some<T>(T value)
        {
            return new Optional<T>(value);
        }

        public static Optional<T> From<T>(T value)
        {
            return (value == null) ? None<T>() : Some(value);
        }

        public static Optional<T> From<T>(T value, Predicate<T> condition)
        {
            if (value == null) return None<T>();

            return condition(value) ? Some(value) : None<T>();
        }
    }

    public static class OptionalCreateExtensions
    {
        public static Optional<T> ToSome<T>(this T value)
        {
            return Optional.Some(value);
        }

        public static Optional<T> ToNone<T>(this T value)
        {
            return Optional.None<T>();
        }


        public static Optional<T> ToOptional<T>(this T value)
        {
            return Optional.From(value);
        }

        public static Optional<T> ToOptional<T>(this T value, Predicate<T> condition)
        {
            return Optional.From(value, condition);
        }
    }
}