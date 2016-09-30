using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fluentOptionals.Tests
{
    [TestClass]
    public class OptionalTest
    {
        [TestMethod]
        public void None_ReturnsNoneOptional()
        {
            Optional.None<int>().ShouldBeNone();
        }

        [TestMethod]
        public void Some_ReturnsSomeOptional()
        {
            Optional.Some(1).ShouldBeSome();
        }

        [TestMethod]
        public void Some_WhenSomeIsCalledWithNull_ThenArgumentNullExceptionGetsThrown()
        {
            Action creatingSomeWithNull = () => Optional.Some<string>(null);
            creatingSomeWithNull.ShouldThrow<ArgumentNullException>();
        }
    }
}