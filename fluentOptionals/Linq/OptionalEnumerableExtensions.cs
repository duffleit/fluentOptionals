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

            return takeOne.Any() ? Optional.From(takeOne.First()) : Optional.None<T>();
        }

        public static Optional<T> FirstOrNone<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return source.Where(predicate).FirstOrNone();
        }

        public static Optional<T> LastOrNone<T>(this IEnumerable<T> source)
        {
            var enumerable = source as T[] ?? source.ToArray();
            var lastOne = enumerable.Skip(Math.Max(0, enumerable.Length - 1)).ToList();

            return lastOne.Any() ? Optional.From(lastOne.First()) : Optional.None<T>();
        }

        public static Optional<T> LastOrNone<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return source.Where(predicate).LastOrNone();
        }

        public static Optional<T> SingleOrNone<T>(this IEnumerable<T> source)
        {
            var takeTwo = source.Take(2).ToArray();

            return takeTwo.Length == 1 ? Optional.From(takeTwo.First()) : Optional.None<T>();
        }

        public static Optional<T> SingleOrNone<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return source.Where(predicate).SingleOrNone();
        }

        public static IEnumerable<Optional<T>> ToOptionalList<T>(this IEnumerable<T> source)
            => source.Select(Optional.From);


        public static IEnumerable<Optional<T>> ToOptionalList<T>(this IEnumerable<T> source, Func<T, bool> condition)
            => source.Select(s => Optional.From(s, condition));
    }
}