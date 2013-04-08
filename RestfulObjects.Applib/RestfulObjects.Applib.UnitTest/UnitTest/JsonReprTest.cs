using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace RestfulObjects.Applib.UnitTest
{
    [TestClass]
    public class JsonReprTest
    {
        [TestMethod]
        public void FromResource()
        {
            var jsonRepr = JsonRepr.FromResource<HomePageRepr>(Properties.Resources.HomePage);

            jsonRepr.Should().NotBeNull();
            jsonRepr.Links.Count().Should().Be(5);
        }
    }
}
