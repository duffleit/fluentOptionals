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
            Assert.IsTrue(x.SomeIfNotNull().IsNone());
        }

        [TestMethod]
        public void SomeIfNotNull_WhenNotNullIsGiven_ThenSomeGetsReturned()
        {
            var x = "test";
            Assert.IsTrue(x.SomeIfNotNull().IsSome());
        }

        [TestMethod]
        public void SomeIfNotNull_WhenEmptyStringIsGiven_ThenSomeGetsReturned()
        {
            var x = String.Empty;
            Assert.IsTrue(x.SomeIfNotNull().IsSome());
        }

        #endregion

        #region SomeIf

        [TestMethod]
        public void SomeIf_WhenPredicateIsTrue_ThenSomeGetsReturned()
        {
            var value = 10;
            Assert.IsTrue(value.SomeIf(x => x == 10).IsSome());
        }

        [TestMethod]
        public void SomeIf_WhenPredicateIsFalse_ThenNoneGetsReturned()
        {
            var value = 20;
            Assert.IsTrue(value.SomeIf(x => x == 10).IsNone());
        }

        #endregion
    }
}