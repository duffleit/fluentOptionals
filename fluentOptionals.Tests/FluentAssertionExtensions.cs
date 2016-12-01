using System;
using FluentAssertions;

namespace FluentOptionals.Tests
{
    public static class FluentAssertionExtensions
    {
        public static void ShouldBeNone<T>(this Optional<T> optional)
            => (!optional.IsSome()).Should().BeTrue();

        public static void ShouldBeSome<T>(this Optional<T> optional)
            => optional.IsSome().Should().BeTrue();
    }

    public static class TestHelper
    {
        private static readonly Func<bool> False = () => false;

        public static bool IsSome<T1>(this Optional<T1> optional)
        {
            return optional.Match(t1 => true, False);
        }
    }
}