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
            first.MatchSome(i => i.Should().Be(1));
        }


        [Test]
        public void FirstOrNone_WhenListWithSingleItemIsGiven_ThenSomeOfSingleElementGetsReturned()
        {
            var first = new List<int> { 10 }.FirstOrNone();

            first.ShouldBeSome();
            first.MatchSome(i => i.Should().Be(10));
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
            last.MatchSome(i => i.Should().Be(3));
        }


        [Test]
        public void LastOrNone_WhenListWithSingleItemIsGiven_ThenSomeOfSingleElementGetsReturned()
        {
            var last = new List<int> { 10 }.LastOrNone();

            last.ShouldBeSome();
            last.MatchSome(i => i.Should().Be(10));
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
            single.MatchSome(i => i.Should().Be(10));
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
            var optionalList = list.ToOptionalList();

            optionalList.Should().HaveSameCount(list);
            optionalList.First().GetType().Should().Be(typeof (Optional<int>));
        }

        [Test]
        public void ToOptionalList_WhenListIncludesValuesAndNulls_ThenListOfSomesAndNonesGetsReturned()
        {
            var list = new List<string>() { "1", "2", null, null, "3", null, "4", "5" };
            var optionalList = list.ToOptionalList();

            optionalList.Should().HaveSameCount(list);
            optionalList.Where(o => o.IsSome).Should().HaveCount(5);
            optionalList.Where(o => o.IsNone).Should().HaveCount(3);
            optionalList.First().GetType().Should().Be(typeof(Optional<string>));
        }

        [Test]
        public void ToOptionalList_WhenPredicateIsApplied_ThenListOfSomesAndNonesGetsReturned()
        {
            var list = new List<string>() { "1", "2", null, null, "3", null, "4", "2" };
            var optionalList = list.ToOptionalList(i => i == "2");

            optionalList.Should().HaveSameCount(list);
            optionalList.Where(o => o.IsSome).Should().HaveCount(2);
            optionalList.Where(o => o.IsNone).Should().HaveCount(6);
            optionalList.First().GetType().Should().Be(typeof(Optional<string>));
        }

        #endregion
    }
}