using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using RestfulObjects.Applib.RestSharp;

namespace RestfulObjects.Applib.IntegTest.Bomi4
{
    /// <summary>
    /// Prerequisites: have a RO server running on localhost:7070
    /// </summary>
    [TestClass]
    public class HomePageTest
    {
        private ROClientUsingRestSharp _client;

        [TestInitialize]
        public void Setup()
        {
            _client = new ROClientUsingRestSharp("http://localhost:7070") { Credentials = new NetworkCredential("sven", "pass") };
        }

        [TestMethod]
        public void Get()
        {

            var homePageRepr = _client.HomePage();
            homePageRepr.Should().NotBeNull();

            var link = homePageRepr.Links.Single(l => l.Rel == "self");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/");

            link = homePageRepr.Links.Single(l => l.Rel == "urn:org.restfulobjects:rels/services");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/services");

            link = homePageRepr.Links.Single(l => l.Rel == "urn:org.restfulobjects:rels/version");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/version");

            link = homePageRepr.Links.Single(l => l.Rel == "urn:org.restfulobjects:rels/user");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/user");

            link = homePageRepr.Links.Single(l => l.Rel == "urn:org.restfulobjects:rels/domain-types");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/domain-types");
        }

        [TestMethod]
        public void FollowSelf()
        {
            var homePageRepr = _client.HomePage();

            var link = homePageRepr.Links.Single(l => l.Rel == "self");

            var selfRepr = link.Follow<GenericRepr>(_client).CastTo<HomePageRepr>();

            selfRepr.Links.Single(l => l.Rel == "self").Href.Should().Be("http://localhost:7070/");
            selfRepr.Links.Count.Should().Be(5);
        }

        [TestMethod]
        public void FollowServices()
        {
            var homePageRepr = _client.HomePage();

            var link = homePageRepr.Links.Single(l => l.Rel == "urn:org.restfulobjects:rels/services");

            var servicesRepr = link.Follow<GenericRepr>(_client).CastTo<ListRepr>();

            servicesRepr.Links.Single(l => l.Rel == "self").Href.Should().Be("http://localhost:7070/services");
            servicesRepr.Links.Single(l => l.Rel == "up").Href.Should().Be("http://localhost:7070/");
        }

    }
}
