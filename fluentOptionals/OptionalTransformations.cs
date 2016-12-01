using System;

namespace FluentOptionals
{
    public static class OptionalTransformations
    {
        public static Optional<T> Filter<T>(this Optional<T> optional, Func<T, bool> condition)
            => optional.Match(v => condition(v) ? Optional.None<T>() : Optional.From(v), Optional.None<T>);

        public static Optional<TMapResult> Map<T, TMapResult>(this Optional<T> optional, Func<T, TMapResult> mapper)
            => optional.Match(v => Optional.From(mapper(v)), Optional.None<TMapResult>);
    }
}