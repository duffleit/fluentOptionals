using FluentAssertions;
using NUnit.Framework;

namespace FluentOptionals.Tests
{
    [TestFixture]
    public class Optional2Tests
    {
        [Test]
        public void Match_WhenAllOptionalsAreSome_ThenSomeHandleGetsCalled()
        {
            var someHandleCalled = false;
            var noneHandleCalled = false;

            Optional.From(1)
                    .Join(2)
                    .Match(
                        some: (p1, p2) => someHandleCalled = true,
                        none: () => noneHandleCalled = true
                    );

            someHandleCalled.Should().BeTrue();
            noneHandleCalled.Should().BeFalse();
        }

        [Test]
        public void Match_WhenOneOptionalsNone_ThenNoneHandleGetsCalled()
        {
            var someHandleCalled = false;
            var noneHandleCalled = false;

            Optional.From(1)
                    .Join(Optional.None<string>())
                    .Match(
                        some: (p1, p2) => someHandleCalled = true,
                        none: () => noneHandleCalled = true
                    );

            noneHandleCalled.Should().BeTrue();
            someHandleCalled.Should().BeFalse();
        }

        [Test]
        public void Match_WhenAllOptionalsAreSome_ThenHandleGetsRightParameters()
        {
            var someHandleCalled = false;
            var noneHandleCalled = false;

            Optional.From(1)
                    .Join(2)
                    .Match(
                        some: (p1, p2) =>
                        {
                            someHandleCalled = true;
                            p1.Should().Be(1);
                            p2.Should().Be(2);
                        },
                        none: () => noneHandleCalled = true
                    );

            someHandleCalled.Should().BeTrue();
            noneHandleCalled.Should().BeFalse();
        }

        [Test]
        public void Match_WhenAllOptionalsAreSome_ThenMatchReturnsSomeValue()
        {
            var x =
                Optional.From(1)
                        .Join(2)
                        .Match(
                            some: (p1, p2) => "some",
                            none: () => "none"
                        );

            x.Should().Be("some");
        }


        [Test]
        public void Match_WhenOneOptionalIsNone_ThenMatchReturnsNoneValue()
        {
            var x =
                Optional.From(1)
                        .Join(Optional.None<int>())
                        .Match(
                            some: (p1, p2) => "some",
                            none: () => "none"
                        );

            x.Should().Be("none");
        }

        [Test]
        public void IfNone_WhenOneOptionalIsNone_ThenIfSomeHandleGetsCalled()
        {
            var noneHandleCalled = false;

            Optional.From(1)
                .Join(Optional.None<int>())
                .IfNone(() => noneHandleCalled = true);

            noneHandleCalled.Should().BeTrue();
        }

        [Test]
        public void IfSome_WhenAllOptionalsAreSome_ThenIfSomeHandleGetsCalled()
        {
            var someHandleCalled = false;

            Optional.From(1)
                .Join(2)
                .IfSome((p1, p2) => someHandleCalled = true);

            someHandleCalled.Should().BeTrue();
        }

        [Test]
        public void IsNone_WhenOneOptionalIsNone_ThenIsNoneIsTrue()
        {
            Optional.From(1)
                .Join(Optional.None<int>())
                .IsNone.Should().BeTrue();
        }

        [Test]
        public void IsSome_WhenAlOptionalsAreSome_ThenIsSomeIsTrue()
        {
            Optional.From(1)
                .Join(2)
                .IsSome.Should().BeTrue();
        }
    }
}