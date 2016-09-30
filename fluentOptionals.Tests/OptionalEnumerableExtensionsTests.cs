using System.Collections.Generic;
using fluentOptionals.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fluentOptionals.Tests
{
    [TestClass]
    public class OptionalEnumerableExtensionsTests
    {
        #region FirstOrNone (IEnumerable<T>)

        [TestMethod]
        public void FirstOrNone_WhenEmptyListIsGiven_ThenNoneGetsReturned()
        {
            new List<int>().FirstOrNone().ShouldBeNone();
        }

        [TestMethod]
        public void FirstOrNone_WhenListWithMultipleItemsIsGiven_ThenSomeOfFirstElementGetsReturned()
        {
            var first = new List<int> {1, 2, 3}.FirstOrNone();

            first.ShouldBeSome();
            first.IfSome(i => i.Should().Be(1));
        }


        [TestMethod]
        public void FirstOrNone_WhenListWithSingleItemIsGiven_ThenSomeOfSingleElementGetsReturned()
        {
            var first = new List<int> { 10 }.FirstOrNone();

            first.ShouldBeSome();
            first.IfSome(i => i.Should().Be(10));
        }

        #endregion

        #region LastOrNone (IEnumerable<T>)

        [TestMethod]
        public void LastOrNone_WhenEmptyListIsGiven_ThenNoneGetsReturned()
        {
            new List<int>().LastOrNone().ShouldBeNone();
        }

        [TestMethod]
        public void LastOrNone_WhenListWithMultipleItemsIsGiven_ThenSomeOfLastElementGetsReturned()
        {
            var last = new List<int> { 1, 2, 3 }.LastOrNone();

            last.ShouldBeSome();
            last.IfSome(i => i.Should().Be(3));
        }


        [TestMethod]
        public void LastOrNone_WhenListWithSingleItemIsGiven_ThenSomeOfSingleElementGetsReturned()
        {
            var last = new List<int> { 10 }.LastOrNone();

            last.ShouldBeSome();
            last.IfSome(i => i.Should().Be(10));
        }

        #endregion

        #region SingleOrNone (IEnumerable<T>)

        [TestMethod]
        public void SingleOrNone_WhenEmptyListIsGiven_ThenNoneGetsReturned()
        {
            new List<int>().SingleOrNone().ShouldBeNone();
        }

        [TestMethod]
        public void SingleOrNone_WhenListWithMultipleItemsIsGiven_ThenNoneGetsReturned()
        {
            new List<int> { 1, 2, 3 }.SingleOrNone().ShouldBeNone();
        }


        [TestMethod]
        public void SingleOrNone_WhenListWithSingleItemIsGiven_ThenSomeOfSingleElementGetsReturned()
        {
            var single = new List<int> { 10 }.SingleOrNone();

            single.ShouldBeSome();
            single.IfSome(i => i.Should().Be(10));
        }

        #endregion

        #region ToOptionalList (IEnumerable<T>)

        public void ToOptionalList_WhenListIsEmpty_ThenEmptyListGetsReturned()
        {
            new List<int>().ToOptionalList().Should().HaveCount(0);
        }

        public void ToOptionalList_WhenListIncludesValues_ThenListOfOptionalsGetsReturned()
        {
            var list = new List<int>() {1, 2, 3, 4, 5};
            var optionalList = list.ToOptionalList();

            optionalList.Should().HaveSameCount(list);
            optionalList.GetType().Should().Be(typeof (Optional<int>));
        }

        public void ToOptionalList_WhenListIncludesValuesAndNulls_ThenListOfSomesAndNonesGetsReturned()
        {
            var list = new List<string>() { "1", "2", null, null, "3", null, "4", "5" };
            var optionalList = list.ToOptionalList();

            optionalList.Should().HaveCount(8);
            optionalList.AreSome().Should().HaveCount(5);
            optionalList.AreNone().Should().HaveCount(3);
            optionalList.GetType().Should().Be(typeof(Optional<string>));
        }

        #endregion

        #region Map (IEnumerable<Optional<T>)

        public void Map_WhenListIsEmpty_ThenEmptyListGetsReturned()
        {
            new List<Optional<int>>().Map(i => i.ToString()).Should().HaveCount(0);
        }

        public void Map_WhenListIncludesValues_ThenMappedListOfOptionalsGetsReturned()
        {
            var list = new List<Optional<int>>() { 1, 2, 3, 4, 5 };
            var optionalList = list.Map(i => i.ToString());

            optionalList.Should().HaveSameCount(list);
            optionalList.GetType().Should().Be(typeof(Optional<string>));
        }

        public void Map_WhenListIncludesValuesAndNones_ThenMappedListOfSomesAndNonesGetsReturned()
        {
            var list = new List<Optional<string>>() { "1", "2", Optional.None<string>(), Optional.None<string>(), "3", Optional.None<string>(), "4", "5" };
            var optionalList = list.Map(i => i.ToString());

            optionalList.Should().HaveCount(8);
            optionalList.AreSome().Should().HaveCount(5);
            optionalList.AreNone().Should().HaveCount(3);
            optionalList.GetType().Should().Be(typeof(Optional<string>));
        }

        #endregion 

        #region AreSome (IEnumerable<Optional<T>>)

        [TestMethod]
        public void AreSome_WhenEmptyListIsGiven_ThenEmptyListGetsReturned()
        {
            new List<Optional<int>>().AreSome().Should().HaveCount(0);
        }

        [TestMethod]
        public void AreSome_WhenListOnlyIncludeSomeItems_ThenAllElementsGetReturned()
        {
            var list = new List<Optional<int>>
            {
                Optional.Some(10),
                Optional.Some(20),
            };

            list.AreSome().Should().HaveSameCount(list);
        }

        [TestMethod]
        public void AreSome_WhenListOnlyIncludeNoneItems_ThenEmptyListGetsReturned()
        {
            var list = new List<Optional<int>>
            {
                Optional.None<int>(),
                Optional.None<int>(),
            };

            list.AreSome().Should().HaveCount(0);
        }

        [TestMethod]
        public void AreSome_WhenListIncludeNoneAndSomeItems_ThenOnlySomeItemsGetReturned()
        {
            var list = new List<Optional<int>>
            {
                Optional.None<int>(),
                Optional.None<int>(),
                Optional.Some(10),
                Optional.None<int>(),
                Optional.Some(20),
                Optional.None<int>()
            };

            list.AreSome().Should().HaveCount(2);
        }

        #endregion

        #region AreNone (IEnumerable<Optional<T>>)

        [TestMethod]
        public void AreNone_WhenEmptyListIsGiven_ThenEmptyListGetsReturned()
        {
            new List<Optional<int>>().AreNone().Should().HaveCount(0);
        }

        [TestMethod]
        public void AreNone_WhenListOnlyIncludeSomeItems_ThenEmptyListGetsReturned()
        {
            var list = new List<Optional<int>>
            {
                Optional.Some(10),
                Optional.Some(20),
            };

            list.AreNone().Should().HaveCount(0);
        }

        [TestMethod]
        public void AreNone_WhenListOnlyIncludeNoneItems_ThenAllElementsGetReturned()
        {
            var list = new List<Optional<int>>
            {
                Optional.None<int>(),
                Optional.None<int>(),
            };

            list.AreNone().Should().HaveCount(2);
        }

        [TestMethod]
        public void AreNone_WhenListIncludeNoneAndSomeItems_ThenOnlyNoneItemsGetReturned()
        {
            var list = new List<Optional<int>>
            {
                Optional.None<int>(),
                Optional.None<int>(),
                Optional.Some(10),
                Optional.None<int>(),
                Optional.Some(20),
                Optional.None<int>()
            };

            list.AreNone().Should().HaveCount(4);
        }

        #endregion
    }
}