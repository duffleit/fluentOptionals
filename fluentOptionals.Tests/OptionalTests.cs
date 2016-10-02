using System;
using fluentOptionals.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fluentOptionals.Tests
{
    [TestClass]
    public class OptionalTests
    {
        #region ToNone

        [TestMethod]
        public void None_ReturnsNoneOptional()
        {
            10.ToNone().ShouldBeNone();
        }

        [TestMethod]
        public void None_WhenNoneIsCreatedFromNull_ThenNoExceptionShouldBeThrown()
        {
            ((object) null).ToNone().ShouldBeNone();
        }

        #endregion 

        #region ToSome

        [TestMethod]
        public void Some_ReturnsSomeOptional()
        {
            10.ToSome().ShouldBeSome();
        }

        [TestMethod]

        public void ToSome_WhenNullIsGiven_ThenSomeCreationFailedExceptionGetsThrown()
        {
            Action nullToSome = () => ((object) null).ToSome();

            nullToSome.ShouldThrow<SomeCreationWithNullException>();
        }

        #endregion

        #region ToOptional

        [TestMethod]
        public void ToOptional_WhenNullIsGiven_ThenNoneGetsReturned()
        {
            ((object) null).ToOptional().ShouldBeNone();
        }

        [TestMethod]
        public void ToOptional_WhenValuesGiven_ThenSomeGetsReturned()
        {
            "test".ToOptional().ShouldBeSome();
        }

        [TestMethod]
        public void ToOptional_WhenEmptyStringIsGiven_ThenSomeGetsReturned()
        {
            string.Empty.ToOptional().ShouldBeSome();
        }

        [TestMethod]
        public void ToOptional_WhenPredicateIsTrue_ThenSomeGetsReturned()
        {
            10.ToOptional(x => x == 10).ShouldBeSome();
        }

        [TestMethod]
        public void ToOptional_WhenPredicateIsFalse_ThenNoneGetsReturned()
        {
            20.ToOptional(x => x == 10).ShouldBeNone();
        }

        [TestMethod]
        public void ToOptional_WhenPredicateForNullIsGiven_ThenNoneGetsReturned()
        {
            ((string) null).ToOptional(x => x.Length == 10).ShouldBeNone();
        }

        #endregion
    }
}