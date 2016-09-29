using System;
using System.Collections.Generic;
using System.Linq;

namespace NOptional
{
    public static class OptionalEnumerableExtensions
    {
        public static Optional<T> FirstOrNone<T>(this IEnumerable<T> source)
        {
            var takeOne = source.Take(1).ToArray();

            return (takeOne.Any()) ? Optional.Some(takeOne.First()) : Optional.None<T>();
        }

        public static Optional<T> LastOrNone<T>(this IEnumerable<T> source)
        {
            var enumerable = source as T[] ?? source.ToArray();
            var lastOne = enumerable.Skip(Math.Max(0, enumerable.Count() - 1)).ToList();

            return (lastOne.Any()) ? Optional.Some(lastOne.First()) : Optional.None<T>();
        }

        public static Optional<T> SingleOrNone<T>(this IEnumerable<T> source)
        {
            var takeTwo = source.Take(2).ToArray();

            return (takeTwo.Length == 1) ? Optional.Some(takeTwo.First()) : Optional.None<T>();
        }

        public static IEnumerable<Optional<T>> ToOptionalList<T>(this IEnumerable<T> source)
            => source.Select(item => new Optional<T>(item));
        
        public static IEnumerable<Optional<T>> AreSome<T>(this IEnumerable<Optional<T>> source) 
            => source.Where(item => item.IsSome);

        public static IEnumerable<Optional<T>> AreNone<T>(this IEnumerable<Optional<T>> source) 
            => source.Where(item => item.IsNone);

        public static IEnumerable<Optional<TMapResult>> Map<TMapResult, T>(this IEnumerable<Optional<T>> source, Func<T, TMapResult> mapper) 
            => source.Select(item => item.Map(mapper));
    }
}