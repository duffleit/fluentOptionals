using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NOptional.Tests
{
    [TestClass]
    public class OptionalCreationExtensionsTests
    {
        #region SomeIfNotNull

        [TestMethod]
        public void SomeIfNotNull_WhenNullIsGiven_ThenNoneGetsReturned()
        {
            object x = null;
            x.SomeIfNotNull().ShouldBeNone();
        }

        [TestMethod]
        public void SomeIfNotNull_WhenNotNullIsGiven_ThenSomeGetsReturned()
        {
            var x = "test";
            x.SomeIfNotNull().ShouldBeSome();
        }

        [TestMethod]
        public void SomeIfNotNull_WhenEmptyStringIsGiven_ThenSomeGetsReturned()
        {
            var x = String.Empty;
            x.SomeIfNotNull().ShouldBeSome();
        }

        #endregion

        #region SomeIf

        [TestMethod]
        public void SomeIf_WhenPredicateIsTrue_ThenSomeGetsReturned()
        {
            var value = 10;
            value.SomeIf(x => x == 10).ShouldBeSome();
        }

        [TestMethod]
        public void SomeIf_WhenPredicateIsFalse_ThenNoneGetsReturned()
        {
            var value = 20;
            value.SomeIf(x => x == 10).ShouldBeNone();
        }

        #endregion
    }
}