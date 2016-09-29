using FluentAssertions;

namespace NOptional.Tests
{
    public static class FluentAssertionExtensions
    {
        public static void ShouldBeNone<T>(this Optional<T> optional)
            => optional.IsNone.Should().BeTrue();

        public static void ShouldBeSome<T>(this Optional<T> optional)
            => optional.IsSome.Should().BeTrue();
    }
}