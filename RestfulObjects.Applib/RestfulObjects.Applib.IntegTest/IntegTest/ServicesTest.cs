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
    public class ServicesTest
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
            var servicesListRepr = _client.Services();
            servicesListRepr.Should().NotBeNull();

            var link = servicesListRepr.Links.Single(l =>  l.Rel == "self");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/services");

            link = servicesListRepr.Links.Single(l => l.Rel == "up");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/");

            servicesListRepr.Value.Count.Should().BeGreaterOrEqualTo(1);
            var serviceLinks = servicesListRepr.Value;

            // the official way, using a Rel
            LinkRepr customerServiceLinkUsingRel = serviceLinks.Single(l => l.Rel == "urn:org.restfulobjects:rels/service;serviceId=\"Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices\"");
            customerServiceLinkUsingRel.Should().NotBeNull();
            customerServiceLinkUsingRel.Href.Should().Be("http://localhost:7070/services/Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices");

            // the hacky way
            LinkRepr customerServiceLinkUsingTitle = serviceLinks.Single(l => l.Title == "Customers");
            customerServiceLinkUsingTitle.Should().NotBeNull();
        }

        [TestMethod]
        public void FollowSelf()
        {
            var servicesListRepr = _client.Services();

            var link = servicesListRepr.Links.Single(l => l.Rel == "self");

            var selfRepr = link.Follow<GenericRepr>(_client).CastTo<ListRepr>();

            selfRepr.Links.Single(l => l.Rel == "self").Href.Should().Be("http://localhost:7070/services");
            selfRepr.Links.Count.Should().Be(2);
        }

        [TestMethod]
        public void FollowCustomerServices()
        {
            var servicesListRepr = _client.Services();
            var serviceLinks = servicesListRepr.Value;

            var link = serviceLinks.Single(l => l.Rel == "urn:org.restfulobjects:rels/service;serviceId=\"Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices\"");

            var customerServiceRepr = link.Follow<GenericRepr>(_client).CastTo<ObjectRepr>();

            customerServiceRepr.Links.Single(l => l.Rel == "self").Href.Should().Be("http://localhost:7070/services/Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices");
            customerServiceRepr.Title.Should().Be("Customers");
            customerServiceRepr.ServiceId.Should().Be("Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices");
        }

    }
}
