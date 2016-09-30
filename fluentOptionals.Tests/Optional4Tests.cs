using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NOptional.Tests
{
    [TestClass]
    public class Optional4Tests
    {
        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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


        [TestMethod]
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

        [TestMethod]
        public void IfNone_WhenOneOptionalIsNone_ThenIfSomeHandleGetsCalled()
        {
            var noneHandleCalled = false;

            Optional.From(1)
                .Join(2)
                .Join(3)
                .Join(Optional.None<int>())
                .IfNone(() => noneHandleCalled = true);

            noneHandleCalled.Should().BeTrue();
        }

        [TestMethod]
        public void IfSome_WhenAllOptionalsAreSome_ThenIfSomeHandleGetsCalled()
        {
            var someHandleCalled = false;

            Optional.From(1)
                .Join(2)
                .Join(3)
                .Join(4)
                .IfSome((p1, p2, p3, p4) => someHandleCalled = true);

            someHandleCalled.Should().BeTrue();
        }
    }
}