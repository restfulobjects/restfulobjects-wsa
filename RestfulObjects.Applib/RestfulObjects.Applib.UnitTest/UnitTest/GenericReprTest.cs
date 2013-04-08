using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace RestfulObjects.Applib.UnitTest
{
    [TestClass]
    public class GenericReprTest
    {
        [TestMethod]
        public void As()
        {
            var genericRepr = JsonRepr.FromResource(Properties.Resources.HomePage);

            var homePageRepr = genericRepr.CastTo<HomePageRepr>();

            homePageRepr.Links.Count().Should().Be(5);
            homePageRepr.Links[0].Rel.Should().Be("self");
            homePageRepr.Links.Single(l => l.Rel == "self").Rel.Should().Be("self");
        }

        public class ListRepr : JsonRepr
        {
            public GenericRepr List { get; set; }
        }

        [TestMethod]
        public void ListOfLinks()
        {
            var someRepr = JsonRepr.FromResource(Properties.Resources.GenericRepr_ListOfLinks).CastTo<ListRepr>();

            var genericRepr = someRepr.List;
            var listOfLinksRepr = genericRepr.CastToList<LinkRepr>();

            listOfLinksRepr.Count().Should().Be(5);
        }


        [TestMethod]
        public void ListOfStrings()
        {
            var someRepr = JsonRepr.FromResource(Properties.Resources.GenericRepr_ListOfStrings).CastTo<ListRepr>();

            var genericRepr = someRepr.List;
            var listOfLinksRepr = genericRepr.CastToList<ScalarRepr>();

            listOfLinksRepr.Count().Should().Be(3);
        }


    }
}
