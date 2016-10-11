using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentOptionals.Linq
{
    public static class OptionalEnumerableExtensions
    {
        public static Optional<T> FirstOrNone<T>(this IEnumerable<T> source)
        {
            var takeOne = source.Take(1).ToArray();

            return takeOne.Any() ? Optional.Some(takeOne.First()) : Optional.None<T>();
        }

        public static Optional<T> LastOrNone<T>(this IEnumerable<T> source)
        {
            var enumerable = source as T[] ?? source.ToArray();
            var lastOne = enumerable.Skip(Math.Max(0, enumerable.Length - 1)).ToList();

            return lastOne.Any() ? Optional.Some(lastOne.First()) : Optional.None<T>();
        }

        public static Optional<T> SingleOrNone<T>(this IEnumerable<T> source)
        {
            var takeTwo = source.Take(2).ToArray();

            return takeTwo.Length == 1 ? Optional.Some(takeTwo.First()) : Optional.None<T>();
        }

        public static IEnumerable<Optional<T>> ToOptionalList<T>(this IEnumerable<T> source)
            => source.Select(Optional.From);


        public static IEnumerable<Optional<T>> ToOptionalList<T>(this IEnumerable<T> source, Func<T, bool> condition)
            => source.Select(s => Optional.From(s, condition));
    }
}