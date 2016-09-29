using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NOptional.Tests
{
    [TestClass]
    public class Optional2Tests
    {
        [TestMethod]
        public void Match_WhenAllOptionalsAreSome_ThenSomeHandleGetsCalled()
        {
            var someHandleCalled = false;
            var noneHandleCalled = false;

            Optional.From(1)
                    .Join("test")
                    .Match(
                        some: (p1, p2)  => someHandleCalled = true,
                        none: ()        => noneHandleCalled = true
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
                    .Join(Optional.None<string>())
                    .Match(
                        some: (p1, p2) => someHandleCalled = true,
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
                    .Join("test")
                    .Match(
                        some: (p1, p2) =>
                        {
                            someHandleCalled = true;
                            p1.GetType().Should().Be(typeof (int));
                            p2.GetType().Should().Be(typeof (string));
                        },
                        none: () => noneHandleCalled = true
                    );

            someHandleCalled.Should().BeTrue();
            noneHandleCalled.Should().BeFalse();
        }
    }
}