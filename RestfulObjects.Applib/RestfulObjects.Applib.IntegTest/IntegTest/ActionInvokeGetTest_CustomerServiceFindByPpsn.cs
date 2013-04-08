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
    public class ActionInvokeGetTest_CustomerServiceFindByPpsn
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
            var findByPpsnDetailsRepr = _client.DomainServiceAction("Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices", "FindByPpsn");
            findByPpsnDetailsRepr.Should().NotBeNull();
            findByPpsnDetailsRepr.Id.Should().Be("FindByPpsn");
            findByPpsnDetailsRepr.Extensions.Single(e => e.Key == "friendlyName").Value.CastTo<ScalarRepr>().AsString().Should().Be("Find By Ppsn");

            findByPpsnDetailsRepr.Parameters.Count.Should().Be(1);
            var ppsnParamRepr = findByPpsnDetailsRepr.Parameters.Single(p => p.Key == "ppsn").Value;

            ppsnParamRepr.Extensions.Single(e => e.Key == "returnType").Value.CastTo<ScalarRepr>().AsString().Should().Be("string");
            ppsnParamRepr.Extensions.Single(e => e.Key == "maxLength").Value.CastTo<ScalarRepr>().AsLong().Should().Be(8);


            var link = findByPpsnDetailsRepr.Links.Single(l => l.Rel == "self");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/services/Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices/actions/FindByPpsn");

            link = findByPpsnDetailsRepr.Links.Single(l => l.Rel == "up");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/services/Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices");

            link = findByPpsnDetailsRepr.Links.Single(l => l.Rel.Contains("invoke"));
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/services/Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices/actions/FindByPpsn/invoke");
            link.Method.Should().Be("GET");
            link.Arguments.Single(a => a.Key == "ppsn").Should().NotBeNull();

            link = findByPpsnDetailsRepr.Links.Single(l => l.Rel.Contains("return-type"));
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/domain-types/Sdm.Cluster.Customers.Impl.Customer");

            link = findByPpsnDetailsRepr.Links.Single(l => l.Rel == "describedby");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/domain-types/Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices/actions/FindByPpsn");

            link = findByPpsnDetailsRepr.Links.Single(l => l.Rel.Contains("action-param") && l.Id == "ppsn");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:7070/domain-types/Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices/actions/FindByPpsn/params/ppsn");

        }

        [TestMethod]
        public void FollowSelf()
        {
            var findByPpsnDetailsRepr = _client.DomainServiceAction("Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices", "FindByPpsn");

            var link = findByPpsnDetailsRepr.Links.Single(l => l.Rel == "self");

            var selfRepr = link.Follow<GenericRepr>(_client).CastTo<ActionRepr>();

            selfRepr.Links.Single(l => l.Rel == "self").Href.Should().Be("http://localhost:7070/services/Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices/actions/FindByPpsn");
            selfRepr.Links.Count.Should().Be(6);
        }

        [TestMethod]
        public void FollowInvokeLink()
        {
            var findByPpsnDetailsRepr = _client.DomainServiceAction("Sdm.Cluster.Customers.Impl.Services.CustomerMenuServices", "FindByPpsn");

            var invokeLink = findByPpsnDetailsRepr.Links.Single(l => l.Rel.Contains("invoke"));
            var args = invokeLink.Arguments;
            args["ppsn"] = new ArgumentNodeForScalarRepr("0100111P");

            var actionResultRepr = invokeLink.Follow<GenericRepr>(_client, args).CastTo<ActionResultRepr>();
            actionResultRepr.ResultType.Should().Be("object");
            actionResultRepr.Result.Should().NotBeNull();

            var customerRepr = actionResultRepr.Result.CastTo<ObjectRepr>();
            customerRepr.DomainType.Should().Be("Sdm.Cluster.Customers.Impl.Customer");
            customerRepr.InstanceId.Should().Be("59");
        }

    }
}
