using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using FluentAssertions;
using RestfulObjects.Applib.WinRT;

namespace RestfulObjects.Applib.IntegTest.Bomi2
{
    /// <summary>
    /// Prerequisites: have a RO server running on 192.168.57.1:8888
    /// </summary>
    [TestClass]
    public class DomainServiceTest_CustomerService
    {
        private ROClientOnWinRT _client;

        [TestInitialize]
        public void Setup()
        {
            _client = new ROClientOnWinRT("http://192.168.57.1:8888") { Credentials = new NetworkCredential("sven", "pass") };
        }

        [TestMethod]
        public void Get()
        {
            var customerServiceRepr = _client.DomainService("sdm.restserver.RestRepositories.CustomerRepository");
            customerServiceRepr.Should().NotBeNull();

            customerServiceRepr.Title.Should().Be("a CustomerRepository");
            customerServiceRepr.ServiceId.Should().Be("sdm.restserver.RestRepositories.CustomerRepository");

            var link = customerServiceRepr.Links.Single(l => l.Rel == "self");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://192.168.57.1:8888/services/sdm.restserver.RestRepositories.CustomerRepository");

            link = customerServiceRepr.Links.Single(l => l.Rel == "describedby");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://192.168.57.1:8888/domain-types/sdm.restserver.RestRepositories.CustomerRepository");

            customerServiceRepr.Extensions.Single(x => x.Key == "isService").Value.CastTo<ScalarRepr>().AsBoolean().Should().Be(true);
            customerServiceRepr.Extensions.Single(x => x.Key == "friendlyName").Value.CastTo<ScalarRepr>().AsString().Should().Be("Customer Repository");

            customerServiceRepr.Members.Count.Should().BeGreaterOrEqualTo(1);

            var findByPpsn = customerServiceRepr.Members.Single(m => m.Key == "FindByPPSN").Value;
            findByPpsn.MemberType.Should().Be("action");
            findByPpsn.Id.Should().Be("FindByPPSN");
            findByPpsn.Links.Count.Should().BeGreaterOrEqualTo(1);

            var findByPpsnDetailsLink = findByPpsn.Links.Single(l => l.Rel.Contains("details"));
            findByPpsnDetailsLink.Should().NotBeNull();
        }

        [TestMethod]
        public void FollowSelf()
        {
            var customerServiceRepr = _client.DomainService("sdm.restserver.RestRepositories.CustomerRepository");

            var link = customerServiceRepr.Links.Single(l => l.Rel == "self");

            var selfRepr = link.Follow<GenericRepr>(_client).CastTo<ObjectRepr>();

            selfRepr.Links.Single(l => l.Rel == "self").Href.Should().Be("http://192.168.57.1:8888/services/sdm.restserver.RestRepositories.CustomerRepository");
            selfRepr.Links.Count.Should().Be(2);
        }

        [TestMethod]
        public void FollowDetailsLink()
        {
            var customerServiceRepr = _client.DomainService("sdm.restserver.RestRepositories.CustomerRepository");

            var findByPpsn = customerServiceRepr.Members.Single(m => m.Key == "FindByPPSN").Value;
            findByPpsn.MemberType.Should().Be("action");
            findByPpsn.Id.Should().Be("FindByPPSN");
            findByPpsn.Links.Count.Should().BeGreaterOrEqualTo(1);

            var findByPpsnDetailsLink = findByPpsn.Links.Single(l => l.Rel.Contains("details"));


            var findByPpsnDetailsRepr = findByPpsnDetailsLink.Follow<GenericRepr>(_client).CastTo<ActionRepr>();

            findByPpsnDetailsRepr.Links.Single(l => l.Rel == "self").Href.Should().Be("http://192.168.57.1:8888/services/sdm.restserver.RestRepositories.CustomerRepository/actions/FindByPPSN");
            findByPpsnDetailsRepr.Id.Should().Be("FindByPPSN");
        
        }

    }
}
