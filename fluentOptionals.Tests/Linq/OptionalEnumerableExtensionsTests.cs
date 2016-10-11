using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentOptionals.Linq;
using NUnit.Framework;

namespace FluentOptionals.Tests.Linq
{
    [TestFixture]
    public class OptionalEnumerableExtensionsTests
    {
        #region FirstOrNone (IEnumerable<T>)

        [Test]
        public void FirstOrNone_WhenEmptyListIsGiven_ThenNoneGetsReturned()
        {
            new List<int>().FirstOrNone().ShouldBeNone();
        }

        [Test]
        public void FirstOrNone_WhenListWithMultipleItemsIsGiven_ThenSomeOfFirstElementGetsReturned()
        {
            var first = new List<int> {1, 2, 3}.FirstOrNone();

            first.ShouldBeSome();
            first.IfSome(i => i.Should().Be(1));
        }

        [Test]
        public void FirstOrNone_WhenListWithSingleItemIsGiven_ThenSomeOfFirstElementGetsReturned()
        {
            var first = new List<int> { 10 }.FirstOrNone();

            first.ShouldBeSome();
            first.IfSome(i => i.Should().Be(10));
        }

        [Test]
        public void FirstOrNone_WhenListWithFristItemWhichIsNullIsGiven_ThenNoneGetsReturned()
        {
            new List<string> { null, string.Empty }.FirstOrNone().ShouldBeNone();
        }

        [Test]
        public void FirstOrNoneWithPredicate_WhenEmptyListIsGiven_ThenNoneGetsReturned()
        {
            new List<int>().FirstOrNone(s => s == 0).ShouldBeNone();
        }

        [Test]
        public void FirstOrNoneWithPredicate_WhenListWithNoMatchingItemsIsGiven_ThenNoneGetsReturned()
        {
            var first = new List<int> { 1, 2, 3 }.FirstOrNone(s => s > 4);

            first.ShouldBeNone();
        }


        [Test]
        public void FirstOrNoneWithPredicate_WhenListWithMatchingItemsIsGiven_ThenFirstSomeGetsReturned()
        {
            var first = new List<int> { 10, 20, 30 }.FirstOrNone(s => s >= 20);

            first.ShouldBeSome();
            first.IfSome(i => i.Should().Be(20));
        }

        #endregion

        #region LastOrNone (IEnumerable<T>)

        [Test]
        public void LastOrNone_WhenEmptyListIsGiven_ThenNoneGetsReturned()
        {
            new List<int>().LastOrNone().ShouldBeNone();
        }

        [Test]
        public void LastOrNone_WhenListWithMultipleItemsIsGiven_ThenSomeOfLastElementGetsReturned()
        {
            var last = new List<int> { 1, 2, 3 }.LastOrNone();

            last.ShouldBeSome();
            last.IfSome(i => i.Should().Be(3));
        }

        [Test]
        public void LastOrNone_WhenListWithSingleItemIsGiven_ThenSomeOfLastElementGetsReturned()
        {
            var last = new List<int> { 10 }.LastOrNone();

            last.ShouldBeSome();
            last.IfSome(i => i.Should().Be(10));
        }

        [Test]
        public void LastOrNoneWithPredicate_WhenEmptyListIsGiven_ThenNoneGetsReturned()
        {
            new List<int>().LastOrNone(s => s == 0).ShouldBeNone();
        }

        [Test]
        public void LastOrNoneWithPredicate_WhenListWithNoMatchingItemsIsGiven_ThenNoneGetsReturned()
        {
            var last = new List<int> { 1, 2, 3 }.LastOrNone(s => s > 4);

            last.ShouldBeNone();
        }

        [Test]
        public void LastOrNone_WhenListWithLastItemWhichIsNullIsGiven_ThenNoneGetsReturned()
        {
            new List<string> { string.Empty, null }.LastOrNone().ShouldBeNone();
        }

        [Test]
        public void LastOrNoneWithPredicate_WhenListWithMatchingItemsIsGiven_ThenLastSomeGetsReturned()
        {
            var last = new List<int> { 10, 20, 30 }.LastOrNone(s => s >= 20);

            last.ShouldBeSome();
            last.IfSome(i => i.Should().Be(30));
        }

        #endregion

        #region SingleOrNone (IEnumerable<T>)

        [Test]
        public void SingleOrNone_WhenEmptyListIsGiven_ThenNoneGetsReturned()
        {
            new List<int>().SingleOrNone().ShouldBeNone();
        }

        [Test]
        public void SingleOrNone_WhenListWithMultipleItemsIsGiven_ThenNoneGetsReturned()
        {
            new List<int> { 1, 2, 3 }.SingleOrNone().ShouldBeNone();
        }


        [Test]
        public void SingleOrNone_WhenListWithSingleItemIsGiven_ThenSomeOfSingleElementGetsReturned()
        {
            var single = new List<int> { 10 }.SingleOrNone();

            single.ShouldBeSome();
            single.IfSome(i => i.Should().Be(10));
        }

        [Test]
        public void SingleOrNone_WhenListWithSingleItemWhichIsNullIsGiven_ThenNoneGetsReturned()
        {
            new List<string> { null }.SingleOrNone().ShouldBeNone();
        }


        [Test]
        public void SingleOrNoneWithPredicate_WhenEmptyListIsGiven_ThenNoneGetsReturned()
        {
            new List<int>().SingleOrNone(s => s == 0).ShouldBeNone();
        }

        [Test]
        public void SingleOrNoneWithPredicate_WhenListWithMultipleItemsIsGiven_ThenNoneGetsReturned()
        {
            new List<int> { 1, 1, 1 }.SingleOrNone(s => s == 1).ShouldBeNone();
        }

        [Test]
        public void SingleOrNoneWithPredicate_WhenListWithSingleItemIsGiven_ThenSomeOfSingleElementGetsReturned()
        {
            var single = new List<int> { 1, 2, 3 }.SingleOrNone(s => s == 2);

            single.ShouldBeSome();
            single.IfSome(i => i.Should().Be(2));
        }

        #endregion

        #region ToOptionalList (IEnumerable<T>)

        [Test]
        public void ToOptionalList_WhenListIsEmpty_ThenEmptyListGetsReturned()
        {
            new List<int>().ToOptionalList().Should().HaveCount(0);
        }

        [Test]
        public void ToOptionalList_WhenListIncludesValues_ThenListOfOptionalsGetsReturned()
        {
            var list = new List<int>() {1, 2, 3, 4, 5};
            var optionalList = list.ToOptionalList().ToList();

            optionalList.Should().HaveSameCount(list);
            optionalList.First().GetType().Should().Be(typeof (Optional<int>));
        }

        [Test]
        public void ToOptionalList_WhenListIncludesValuesAndNulls_ThenListOfSomesAndNonesGetsReturned()
        {
            var list = new List<string>() { "1", "2", null, null, "3", null, "4", "5" };
            var optionalList = list.ToOptionalList().ToList();

            optionalList.Should().HaveSameCount(list);
            optionalList.Where(o => o.IsSome).Should().HaveCount(5);
            optionalList.Where(o => o.IsNone).Should().HaveCount(3);
            optionalList.First().GetType().Should().Be(typeof(Optional<string>));
        }

        [Test]
        public void ToOptionalList_WhenPredicateIsApplied_ThenListOfSomesAndNonesGetsReturned()
        {
            var list = new List<string>() { "1", "2", null, null, "3", null, "4", "2" };
            var optionalList = list.ToOptionalList(i => i == "2").ToList();

            optionalList.Should().HaveSameCount(list);
            optionalList.Where(o => o.IsSome).Should().HaveCount(2);
            optionalList.Where(o => o.IsNone).Should().HaveCount(6);
            optionalList.First().GetType().Should().Be(typeof(Optional<string>));
        }

        #endregion
    }
}