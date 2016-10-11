using FluentAssertions;
using NUnit.Framework;

namespace FluentOptionals.Tests
{
    [TestFixture]
    public class Optional7Tests
    {
        [Test]
        public void Match_WhenAllOptionalsAreSome_ThenSomeHandleGetsCalled()
        {
            var someHandleCalled = false;
            var noneHandleCalled = false;

            Optional.From(1)
                    .Join(2)
                    .Join(3)
                    .Join(4)
                    .Join(5)
                    .Join(6)
                    .Join(7)
                    .Match(
                        some: (p1, p2, p3, p4, p5, p6, p7) => someHandleCalled = true,
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
                    .Join(2)
                    .Join(3)
                    .Join(4)
                    .Join(5)
                    .Join(6)
                    .Join(Optional.None<string>())
                    .Match(
                        some: (p1, p2, p3, p4, p5, p6, p7) => someHandleCalled = true,
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
                    .Join(3)
                    .Join(4)
                    .Join(5)
                    .Join(6)
                    .Join(7)
                    .Match(
                        some: (p1, p2, p3, p4, p5, p6, p7) =>
                        {
                            someHandleCalled = true;
                            p1.Should().Be(1);
                            p2.Should().Be(2);
                            p3.Should().Be(3);
                            p4.Should().Be(4);
                            p5.Should().Be(5);
                            p6.Should().Be(6);
                            p7.Should().Be(7);
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
                        .Join(3)
                        .Join(4)
                        .Join(5)
                        .Join(6)
                        .Join(7)
                        .Match(
                            some: (p1, p2, p3, p4, p5, p6, p7) => "some",
                            none: () => "none"
                        );

            x.Should().Be("some");
        }


        [Test]
        public void Match_WhenOneOptionalIsNone_ThenMatchReturnsNoneValue()
        {
            var x =
                Optional.From(1)
                        .Join(2)
                        .Join(3)
                        .Join(4)
                        .Join(5)
                        .Join(6)
                        .Join(Optional.None<int>())
                        .Match(
                            some: (p1, p2, p3, p4, p5, p6, p7) => "some",
                            none: () => "none"
                        );

            x.Should().Be("none");
        }

        [Test]
        public void IfNone_WhenOneOptionalIsNone_ThenIfSomeHandleGetsCalled()
        {
            var noneHandleCalled = false;

            Optional.From(1)
                .Join(2)
                .Join(3)
                .Join(4)
                .Join(5)
                .Join(6)
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
                .Join(3)
                .Join(4)
                .Join(5)
                .Join(6)
                .Join(7)
                .IfSome((p1, p2, p3, p4, p5, p6, p7) => someHandleCalled = true);

            someHandleCalled.Should().BeTrue();
        }

        [Test]
        public void IsNone_WhenOneOptionalIsNone_ThenIsNoneIsTrue()
        {
            Optional.From(1)
                .Join(2)
                .Join(3)
                .Join(4)
                .Join(5)
                .Join(6)
                .Join(Optional.None<int>())
                .IsNone.Should().BeTrue();
        }

        [Test]
        public void IsSome_WhenAlOptionalsAreSome_ThenIsSomeIsTrue()
        {
            Optional.From(1)
                .Join(2)
                .Join(3)
                .Join(4)
                .Join(5)
                .Join(6)
                .Join(7)
                .IsSome.Should().BeTrue();
        }
    }
}