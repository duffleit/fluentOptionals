using System;
using FluentAssertions;
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
            _some = Optional.Some(1);
            _none = Optional.None<int>();
        }

        #region IfSome

        [TestMethod]
        public void IfSome_HandleIsNotCalledOnNone()
        {
            var handleCalled = false;
            _none.IfSome(_ => handleCalled = true);

            handleCalled.Should().Be(false);
        }

        [TestMethod]
        public void IfSome_HandleIsCalledOnSome()
        {
            var handleCalled = false;
            _some.IfSome(_ => handleCalled = true);

            handleCalled.Should().Be(true);
        }

        [TestMethod]
        public void IfSome_HandleReturnsRightValue()
        {
            var returnedValue = 0;
            _some.IfSome(i => returnedValue = i);

            returnedValue.Should().Be(1);
        }

        #endregion

        #region IfNone

        [TestMethod]
        public void IfNone_HandleIsNotCalledOnSome()
        {
            var handleCalled = false;
            _some.IfNone(() => handleCalled = true);

            handleCalled.Should().Be(false);
        }

        [TestMethod]
        public void IfNone_HandleIsCalledOnNone()
        {
            var handleCalled = false;
            _none.IfNone(() => handleCalled = true);

            handleCalled.Should().Be(true);
        }

        #endregion

        #region ValueOr

        [TestMethod]
        public void IfNone_WhenOptionalIsNone_ThenValueFromHandleGetsReturned()
        {
            _none.ValueOr(() => 20).Should().Be(20);
        }

        [TestMethod]
        public void IfNone_WhenOptionalIsNone_ThenValueGetsReturned()
        {
            _none.ValueOr(20).Should().Be(20);
        }


        [TestMethod]
        public void IfNone_WhenOptionalIsSome_ThenValueFromHandleIsIgnored()
        {
            _some.ValueOr(() => 20).Should().NotBe(20);
        }

        [TestMethod]
        public void IfNone_WhenOptionalIsSome_ThenValueIsIgnored()
        {
            _some.ValueOr(20).Should().NotBe(20);
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

            someHandleCalled.Should().BeTrue();
            noneHandleCalled.Should().BeFalse();
        }

        [TestMethod]
        public void Match_WhenOptionalHasNoValue_NoneHandleGetsCalled()
        {
            bool someHandleCalled = false, noneHandleCalled = false;

            _none.Match(
                some: _ => someHandleCalled = true,
                none: () => noneHandleCalled = true);

            noneHandleCalled.Should().BeTrue();
            someHandleCalled.Should().BeFalse();
        }

        [TestMethod]
        public void Match_WhenOptionalHasValue_SomeHandleGetsReturned()
        {
            _some.Match(
                some: _ => 20,
                none: () => 30).Should().Be(20);
        }

        [TestMethod]
        public void Match_WhenOptionalHasNoValue_NoneHandleGetsReturned()
        {
            _none.Match(
                some: _ => 20,
                none: () => 30).Should().Be(30);
        }

        #endregion

        #region Map

        [TestMethod]
        public void Map_WhenOpationalIsNone_ThenNoneGetsReturnedAgain()
        {
            var noneOptional = Optional.None<int>();

            var resultOptional = noneOptional.Map(_ => "test");

            resultOptional.ShouldBeNone();
            typeof(Optional<string>).Should().Be(resultOptional.GetType());
        }

        [TestMethod]
        public void Map_WhenOpationalIsSome_ThenSomeGetsReturnedAgain()
        {
            var noneOptional = Optional.Some(10);

            var resultOptional = noneOptional.Map(_ => "test");

            resultOptional.ShouldBeSome();
            typeof (Optional<string>).Should().Be(resultOptional.GetType());
        }

        [TestMethod]
        public void Map_WhenMapOpationReturnsNull_ThenNoneGetsReturned()
        {
            var noneOptional = Optional.Some(10);

            var resultOptional = noneOptional.Map<string>(_ => null);

            resultOptional.ShouldBeNone();
            typeof(Optional<string>).Should().Be(resultOptional.GetType());
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

        #endregion 
    }
}
