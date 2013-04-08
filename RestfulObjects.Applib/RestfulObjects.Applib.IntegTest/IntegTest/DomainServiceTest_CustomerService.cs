using System.Collections.Generic;
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
    public class DomainServiceTest_CustomerService
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
            var customerServiceRepr = _client.DomainService("Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices");
            customerServiceRepr.Should().NotBeNull();

            customerServiceRepr.Title.Should().Be("Customers");
            customerServiceRepr.ServiceId.Should().Be("Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices");

            var link = customerServiceRepr.Links.Single(l => l.Rel == "self");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/services/Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices");

            link = customerServiceRepr.Links.Single(l => l.Rel == "describedby");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/domain-types/Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices");

            customerServiceRepr.Extensions.Single(x => x.Key == "isService").Value.CastTo<ScalarRepr>().AsBoolean().Should().Be(true);
            customerServiceRepr.Extensions.Single(x => x.Key == "friendlyName").Value.CastTo<ScalarRepr>().AsString().Should().Be("Customers");

            customerServiceRepr.Members.Count.Should().BeGreaterOrEqualTo(2);

            var findByPpsn = customerServiceRepr.Members.Single(m => m.Key == "FindByPpsn").Value;
            findByPpsn.MemberType.Should().Be("action");
            findByPpsn.Id.Should().Be("FindByPpsn");
            findByPpsn.Links.Count.Should().BeGreaterOrEqualTo(1);

            var findByPpsnDetailsLink = findByPpsn.Links.Single(l => l.Rel.Contains("details"));
            findByPpsnDetailsLink.Should().NotBeNull();
        }

        [TestMethod]
        public void FollowSelf()
        {
            var customerServiceRepr = _client.DomainService("Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices");

            var link = customerServiceRepr.Links.Single(l => l.Rel == "self");

            var selfRepr = link.Follow<GenericRepr>(_client).CastTo<ObjectRepr>();

            selfRepr.Links.Single(l => l.Rel == "self").Href.Should().Be("http://localhost:7070/services/Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices");
            selfRepr.Links.Count.Should().Be(2);
        }

        [TestMethod]
        public void FollowDetailsLink()
        {
            var customerServiceRepr = _client.DomainService("Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices");

            var findByPpsn = customerServiceRepr.Members.Single(m => m.Key == "FindByPpsn").Value;
            findByPpsn.MemberType.Should().Be("action");
            findByPpsn.Id.Should().Be("FindByPpsn");
            findByPpsn.Links.Count.Should().BeGreaterOrEqualTo(1);

            var findByPpsnDetailsLink = findByPpsn.Links.Single(l => l.Rel.Contains("details"));


            var findByPpsnDetailsRepr = findByPpsnDetailsLink.Follow<GenericRepr>(_client).CastTo<ActionRepr>();

            findByPpsnDetailsRepr.Links.Single(l => l.Rel == "self").Href.Should().Be("http://localhost:7070/services/Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices/actions/FindByPpsn");
            findByPpsnDetailsRepr.Id.Should().Be("FindByPpsn");
        
        }

    }
}
