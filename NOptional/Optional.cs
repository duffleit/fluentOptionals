using System;

namespace NOptional
{
    public static class Optional
    {
        public static Optional<T> None<T>()
        {
            return new Optional<T>();
        }

        public static Optional<T> Some<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return From(value);
        }

        public static Optional<T> From<T>(T value)
        {
            return new Optional<T>(value);
        }

        public static Optional<T> From<T>(Optional<T> value)
        {
            return value ?? None<T>();
        }
    }

}