using fluentOptionals.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fluentOptionals.Tests
{
    [TestClass]
    public class OptionalCreationExtensionsTests
    {
        #region ToSomeWhenNotNull

        [TestMethod]
        public void ToSomeWhenNotNull_WhenNullIsGiven_ThenNoneGetsReturned()
        {
            ((object) null).ToSomeWhenNotNull().ShouldBeNone();
        }

        [TestMethod]
        public void ToSomeIfNotNull_WhenNotNullIsGiven_ThenSomeGetsReturned()
        {
            "test".ToSomeWhenNotNull().ShouldBeSome();
        }

        [TestMethod]
        public void ToSomeIfNotNull_WhenEmptyStringIsGiven_ThenSomeGetsReturned()
        {
            string.Empty.ToSomeWhenNotNull().ShouldBeSome();
        }

        #endregion

        #region ToSomeWhen

        [TestMethod]
        public void ToSomeWhen_WhenPredicateIsTrue_ThenSomeGetsReturned()
        {
            10.ToSomeWhen(x => x == 10).ShouldBeSome();
        }

        [TestMethod]
        public void ToSomeWhen_WhenPredicateIsFalse_ThenNoneGetsReturned()
        {
            20.ToSomeWhen(x => x == 10).ShouldBeNone();
        }

        #endregion
    }
}