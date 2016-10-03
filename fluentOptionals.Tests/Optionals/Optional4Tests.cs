using FluentAssertions;
using NUnit.Framework;

namespace FluentOptionals.Tests
{
    [TestFixture]
    public class Optional4Tests
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
                    .Match(
                        some: (p1, p2, p3, p4) => someHandleCalled = true,
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
                    .Join(Optional.None<string>())
                    .Match(
                        some: (p1, p2, p3, p4) => someHandleCalled = true,
                        none: () => noneHandleCalled = true
                    );

            noneHandleCalled.Should().BeTrue();
            someHandleCalled.Should().BeFalse();
        }

        [Test]
        public void Match_WhenAllOptionalsAreSome_ThenHandleGetsRightParamters()
        {
            var someHandleCalled = false;
            var noneHandleCalled = false;

            Optional.From(1)
                    .Join(2)
                    .Join(3)
                    .Join(4)
                    .Match(
                        some: (p1, p2, p3, p4) =>
                        {
                            someHandleCalled = true;
                            p1.Should().Be(1);
                            p2.Should().Be(2);
                            p3.Should().Be(3);
                            p4.Should().Be(4);
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
                        .Match(
                            some: (p1, p2, p3, p4) => "some",
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
                        .Join(Optional.None<int>())
                        .Match(
                            some: (p1, p2, p3, p4) => "some",
                            none: () => "none"
                        );

            x.Should().Be("none");
        }

        [Test]
        public void MatchNone_WhenOneOptionalIsNone_ThenMatchSomeHandleGetsCalled()
        {
            var noneHandleCalled = false;

            Optional.From(1)
                .Join(2)
                .Join(3)
                .Join(Optional.None<int>())
                .MatchNone(() => noneHandleCalled = true);

            noneHandleCalled.Should().BeTrue();
        }

        [Test]
        public void MatchSome_WhenAllOptionalsAreSome_ThenMatchSomeHandleGetsCalled()
        {
            var someHandleCalled = false;

            Optional.From(1)
                .Join(2)
                .Join(3)
                .Join(4)
                .MatchSome((p1, p2, p3, p4) => someHandleCalled = true);

            someHandleCalled.Should().BeTrue();
        }
    }
}