using System;
using FluentAssertions;
using NUnit.Framework;

namespace FluentOptionals.Tests
{
    [TestFixture]
    public class OptionalTests
    {
        #region ToNone

        [Test]
        public void None_ReturnsNoneOptional()
        {
            10.ToNone().ShouldBeNone();
        }

        [Test]
        public void None_WhenNoneIsCreatedFromNull_ThenNoExceptionShouldBeThrown()
        {
            ((object) null).ToNone().ShouldBeNone();
        }

        #endregion 

        #region ToSome

        [Test]
        public void Some_ReturnsSomeOptional()
        {
            10.ToSome().ShouldBeSome();
        }

        [Test]

        public void ToSome_WhenNullIsGiven_ThenSomeCreationFailedExceptionGetsThrown()
        {
            Action nullToSome = () => ((object) null).ToSome();

            nullToSome.ShouldThrow<SomeCreationWithNullException>();
        }

        #endregion

        #region ToOptional

        [Test]
        public void ToOptional_WhenNullIsGiven_ThenNoneGetsReturned()
        {
            ((object) null).ToOptional().ShouldBeNone();
        }

        [Test]
        public void ToOptional_WhenValuesGiven_ThenSomeGetsReturned()
        {
            "test".ToOptional().ShouldBeSome();
        }

        [Test]
        public void ToOptional_WhenEmptyStringIsGiven_ThenSomeGetsReturned()
        {
            string.Empty.ToOptional().ShouldBeSome();
        }

        [Test]
        public void ToOptional_WhenPredicateIsTrue_ThenSomeGetsReturned()
        {
            10.ToOptional(x => x == 10).ShouldBeSome();
        }

        [Test]
        public void ToOptional_WhenPredicateIsFalse_ThenNoneGetsReturned()
        {
            20.ToOptional(x => x == 10).ShouldBeNone();
        }

        [Test]
        public void ToOptional_WhenPredicateForNullIsGiven_ThenNoneGetsReturned()
        {
            ((string) null).ToOptional(x => x.Length == 10).ShouldBeNone();
        }

        #endregion
    }
}