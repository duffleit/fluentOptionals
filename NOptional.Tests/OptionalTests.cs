using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NOptional.Tests
{
    [TestClass]
    public class OptionalTests
    {
        private Optional<int> _some;
        private Optional<int> _none;

        [TestInitialize]
        public void Setup()
        {
            _some = new Optional<int>(1);
            _none = new Optional<int>();
        }

        #region IfSome

        [TestMethod]
        public void IfSome_HandleIsNotCalledOnNone()
        {
            var handleCalled = false;
            _none.IfSome(_ => handleCalled = true);

            Assert.IsFalse(handleCalled);
        }

        [TestMethod]
        public void IfSome_HandleIsCalledOnSome()
        {
            var handleCalled = false;
            _some.IfSome(_ => handleCalled = true);

            Assert.IsTrue(handleCalled);
        }

        [TestMethod]
        public void IfSome_HandleReturnsRightValue()
        {
            var returnedValue = 0;
            _some.IfSome(i => returnedValue = i);

            Assert.AreEqual(1, returnedValue);
        }

        #endregion

        #region IfNone

        [TestMethod]
        public void IfNone_HandleIsNotCalledOnSome()
        {
            var handleCalled = false;
            _some.IfNone(() => handleCalled = true);

            Assert.IsFalse(handleCalled);
        }

        [TestMethod]
        public void IfNone_HandleIsCalledOnNone()
        {
            var handleCalled = false;
            _none.IfNone(() => handleCalled = true);

            Assert.IsTrue(handleCalled);
        }


        [TestMethod]
        public void IfNone_WhenOptionalIsNone_ThenValueFromHandleGetsReturned()
        {
            var x = _none.IfNone(() => 20);
            Assert.AreEqual(20, x);
        }

        [TestMethod]
        public void IfNone_WhenOptionalIsNone_ThenValueGetsReturned()
        {
            var x = _none.IfNone(20);
            Assert.AreEqual(20, x);
        }


        [TestMethod]
        public void IfNone_WhenOptionalIsSome_ThenValueFromHandleIsIgnored()
        {
            var x = _some.IfNone(() => 20);
            Assert.AreEqual(1, x);
        }

        [TestMethod]
        public void IfNone_WhenOptionalIsSome_ThenValueIsIgnored()
        {
            var x = _some.IfNone(20);
            Assert.AreEqual(1, x);
        }

        #endregion

        #region Match

        [TestMethod]
        public void Match_WhenOptionalHasValue_SomeHandleGetsCalled()
        {
            bool someHandleCalled = false, noneHandleCalled = false;

            _some.Match(
                some: _ => someHandleCalled = true,
                none: () => noneHandleCalled = true);

            Assert.IsTrue(someHandleCalled);
            Assert.IsFalse(noneHandleCalled);
        }

        [TestMethod]
        public void Match_WhenOptionalHasNoValue_NoneHandleGetsCalled()
        {
            bool someHandleCalled = false, noneHandleCalled = false;

            _none.Match(
                some: _ => someHandleCalled = true,
                none: () => noneHandleCalled = true);

            Assert.IsFalse(someHandleCalled);
            Assert.IsTrue(noneHandleCalled);
        }

        [TestMethod]
        public void Match_WhenOptionalHasValue_SomeHandleGetsReturned()
        {
            var x = _some.Match(
                some: _ => 20,
                none: () => 30);

            Assert.AreEqual(20, x);
        }

        [TestMethod]
        public void Match_WhenOptionalHasNoValue_NoneHandleGetsReturned()
        {
            var x = _none.Match(
                some: _ => 20,
                none: () => 30);

            Assert.AreEqual(30, x);
        }

        #endregion

        #region Implicit Operator

        [TestMethod]
        public void ImplicitOperator_NullGetsNone()
        {
            Optional<object> optional = (object) null;
            optional.IfSome(_ => Assert.Fail());
        }

        [TestMethod]
        public void ImplicitOperator_ValueTypeGetsSome()
        {
            Optional<int> optional = 15;
            optional.IfNone(() => Assert.Fail());
        }

        [TestMethod]
        public void ImplicitOperator_ReferenceTypeGetsSome()
        {
            Optional<DateTime> optional = DateTime.Now;
            optional.IfNone(() => Assert.Fail());
        }

        #endregion

        #region None & Some

        [TestMethod]
        public void None_ReturnsNoneOptional()
        {
            var isNone = Optional.None<int>().IsNone();
            Assert.IsTrue(isNone);
        }

        [TestMethod]
        public void Some_ReturnsSomeOptional()
        {
            var isSome = Optional.Some(1).IsSome();
            Assert.IsTrue(isSome);
        }

        #endregion
    }
}
