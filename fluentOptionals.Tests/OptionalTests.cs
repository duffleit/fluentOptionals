using System;
using FluentAssertions;
using NUnit.Framework;

namespace FluentOptionals.Tests
{
    #region factory methods tests

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
            nullToSome.Should().Throw<SomeCreationOfNullException>();
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

        [Test]
        public void ToOptional_WhenNullableWithoutValueIsGiven_ThenNoneGetsReturned()
        {
            new int?().ToOptional().ShouldBeNone();
        }

        [Test]
        public void ToOptional_WhenNullableWithValueIsGiven_ThenSomeGetsReturned()
        {
            new int?(10).ToOptional().ShouldBeSome();
        }

        #endregion
    }

    #endregion

    [TestFixture]
    public class Optional1Tests
    {
        private Optional<int> _some;
        private Optional<int> _none;

        [SetUp]
        public void Setup()
        {
            _some = Optional.Some(1);
            _none = Optional.None<int>();
        }

        #region IfSome

        [Test]
        public void IfSome_HandleIsNotCalledOnNone()
        {
            var handleCalled = false;
            _none.IfSome(_ => handleCalled = true);

            handleCalled.Should().Be(false);
        }

        [Test]
        public void IfSome_HandleIsCalledOnSome()
        {
            var handleCalled = false;
            _some.IfSome(_ => handleCalled = true);

            handleCalled.Should().Be(true);
        }

        [Test]
        public void IfSome_HandleReturnsRightValue()
        {
            var returnedValue = 0;
            _some.IfSome(i => returnedValue = i);

            returnedValue.Should().Be(1);
        }

        #endregion

        #region IfNone

        [Test]
        public void IfNone_HandleIsNotCalledOnSome()
        {
            var handleCalled = false;
            _some.IfNone(() => handleCalled = true);

            handleCalled.Should().Be(false);
        }

        [Test]
        public void IfNone_HandleIsCalledOnNone()
        {
            var handleCalled = false;
            _none.IfNone(() => handleCalled = true);

            handleCalled.Should().Be(true);
        }

        #endregion

        #region ValueOr

        [Test]
        public void IfNone_WhenOptionalIsNone_ThenValueFromHandleGetsReturned()
        {
            _none.ValueOr(() => 20).Should().Be(20);
        }

        [Test]
        public void IfNone_WhenOptionalIsNone_ThenValueGetsReturned()
        {
            _none.ValueOr(20).Should().Be(20);
        }


        [Test]
        public void IfNone_WhenOptionalIsSome_ThenValueFromHandleIsIgnored()
        {
            _some.ValueOr(() => 20).Should().NotBe(20);
        }

        [Test]
        public void IfNone_WhenOptionalIsSome_ThenValueIsIgnored()
        {
            _some.ValueOr(20).Should().NotBe(20);
        }

        [Test]
        public void ValueOrThrow_WhenOptionalIsSome_ThenValueGetsReturned()
        {
            _some.ValueOrThrow(new TestException()).Should().NotBe(20);
        }

        [Test]
        public void ValueOrThrow_WhenOptionalIsValue_ThenValueGetsThrown()
        {
            Action noneThrowsException = () => _none.ValueOrThrow(new TestException());

            noneThrowsException.Should().Throw<Exception>();
        }

        #endregion

        #region Match

        [Test]
        public void Match_WhenOptionalHasValue_SomeHandleGetsCalled()
        {
            bool someHandleCalled = false, noneHandleCalled = false;

            _some.Match(
                some: _ => someHandleCalled = true,
                none: () => noneHandleCalled = true);

            someHandleCalled.Should().BeTrue();
            noneHandleCalled.Should().BeFalse();
        }

        [Test]
        public void Match_WhenOptionalHasNoValue_NoneHandleGetsCalled()
        {
            bool someHandleCalled = false, noneHandleCalled = false;

            _none.Match(
                some: _ => someHandleCalled = true,
                none: () => noneHandleCalled = true);

            noneHandleCalled.Should().BeTrue();
            someHandleCalled.Should().BeFalse();
        }

        [Test]
        public void Match_WhenOptionalHasValue_SomeHandleGetsReturned()
        {
            _some.Match(
                some: _ => 20,
                none: () => 30).Should().Be(20);
        }

        [Test]
        public void Match_WhenOptionalHasNoValue_NoneHandleGetsReturned()
        {
            _none.Match(
                some: _ => 20,
                none: () => 30).Should().Be(30);
        }


        [Test]
        public void Match_CanReturnNull()
        {
            _none.Match(
                some: _ => null as string,
                none: () => null as string).Should().Be(null);
        }

        #endregion

        #region Map

        [Test]
        public void Map_WhenOptionalIsNone_ThenNoneGetsReturnedAgain()
        {
            var resultOptional = 10.ToNone().Map(_ => "test");

            resultOptional.ShouldBeNone();
            typeof(Optional<string>).Should().Be(resultOptional.GetType());
        }

        [Test]
        public void Map_WhenOptionalIsSome_ThenSomeGetsReturnedAgain()
        {
            var resultOptional = 10.ToSome().Map(_ => "test");

            resultOptional.ShouldBeSome();
            typeof(Optional<string>).Should().Be(resultOptional.GetType());
        }

        [Test]
        public void Map_WhenMappedOptionalReturnsNull_ThenNoneGetsReturned()
        {
            var resultOptional = 10.ToSome().Map<int, string>(_ => null);

            resultOptional.ShouldBeNone();
            typeof(Optional<string>).Should().Be(resultOptional.GetType());
        }

        #endregion

        #region Shift

        [Test]
        public void Shift_WhenGivenPredicateIsTrue_ThenNoneGetsReturned()
        {
            10.ToSome().Filter(i => i > 5).ShouldBeNone();
        }

        [Test]
        public void Shift_WhenGivenPredicateIsFalse_ThenSomeGetsReturned()
        {
            10.ToSome().Filter(i => i > 15).ShouldBeSome();
        }

        [Test]
        public void Shift_WhenNoneIsShifted_ThenNoneGetsReturned()
        {
            10.ToNone().Filter(i => i > 15).ShouldBeNone();
        }

        #endregion 

        #region Implicit Operator

        [Test]
        public void ImplicitOperator_NullGetsNone()
        {
            Optional<object> optional = (object)null;
            optional.IfSome(_ => Assert.Fail());
        }

        [Test]
        public void ImplicitOperator_ValueTypeGetsSome()
        {
            Optional<int> optional = 15;
            optional.IfNone(Assert.Fail);
        }

        [Test]
        public void ImplicitOperator_ReferenceTypeGetsSome()
        {
            Optional<DateTime> optional = DateTime.Now;
            optional.IfNone(Assert.Fail);
        }

        #endregion
    }

    public class TestException : Exception { }
}