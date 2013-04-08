using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using RestfulObjects.Applib.RestSharp;

namespace RestfulObjects.Applib.IntegTest.Bomi2
{
    /// <summary>
    /// Prerequisites: have a RO server running on localhost:6565
    /// </summary>
    [TestClass]
    public class ActionInvokeGetTest_CustomerServiceFindByPpsn
    {
        private ROClientUsingRestSharp _client;

        [TestInitialize]
        public void Setup()
        {
            _client = new ROClientUsingRestSharp("http://localhost:6565") { Credentials = new NetworkCredential("sven", "pass") };
        }

        [TestMethod]
        public void Get()
        {
            var findByPpsnDetailsRepr = _client.DomainServiceAction("sdm.restserver.RestRepositories.CustomerRepository", "FindByPPSN");
            findByPpsnDetailsRepr.Should().NotBeNull();
            findByPpsnDetailsRepr.Id.Should().Be("FindByPPSN");
            findByPpsnDetailsRepr.Extensions.Single(e => e.Key == "friendlyName").Value.CastTo<ScalarRepr>().AsString().Should().Be("Find By PPSN");

            findByPpsnDetailsRepr.Parameters.Count.Should().Be(1);
            var ppsnParamRepr = findByPpsnDetailsRepr.Parameters.Single(p => p.Key == "PPSN").Value;

            ppsnParamRepr.Extensions.Single(e => e.Key == "returnType").Value.CastTo<ScalarRepr>().AsString().Should().Be("string");
            //ppsnParamRepr.Extensions.Single(e => e.Key == "maxLength").Value.AsLong().Should().Be(0);


            var link = findByPpsnDetailsRepr.Links.Single(l => l.Rel == "self");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:6565/services/sdm.restserver.RestRepositories.CustomerRepository/actions/FindByPPSN");

            link = findByPpsnDetailsRepr.Links.Single(l => l.Rel == "up");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:6565/services/sdm.restserver.RestRepositories.CustomerRepository");

            link = findByPpsnDetailsRepr.Links.Single(l => l.Rel.Contains("invoke"));
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:6565/services/sdm.restserver.RestRepositories.CustomerRepository/actions/FindByPPSN/invoke");
            link.Method.Should().Be("GET");
            link.Arguments.Single(a => a.Key == "PPSN").Should().NotBeNull();

            link = findByPpsnDetailsRepr.Links.Single(l => l.Rel.Contains("return-type"));
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:6565/domain-types/sdm.common.bom.customers.ICustomer");

            link = findByPpsnDetailsRepr.Links.Single(l => l.Rel == "describedby");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:6565/domain-types/sdm.restserver.RestRepositories.CustomerRepository/actions/FindByPPSN");

            link = findByPpsnDetailsRepr.Links.Single(l => l.Rel.Contains("action-param") && l.Id == "PPSN");
            link.Should().NotBeNull();
            link.Href.Should().Be("http://localhost:6565/domain-types/sdm.restserver.RestRepositories.CustomerRepository/actions/FindByPPSN/params/PPSN");

        }

        [TestMethod]
        public void FollowSelf()
        {
            var findByPpsnDetailsRepr = _client.DomainServiceAction("sdm.restserver.RestRepositories.CustomerRepository", "FindByPPSN");

            var link = findByPpsnDetailsRepr.Links.Single(l => l.Rel == "self");

            var selfRepr = link.Follow<GenericRepr>(_client).CastTo<ActionRepr>();

            selfRepr.Links.Single(l => l.Rel == "self").Href.Should().Be("http://localhost:6565/services/sdm.restserver.RestRepositories.CustomerRepository/actions/FindByPPSN");
            selfRepr.Links.Count.Should().Be(6);
        }

        [TestMethod]
        public void FollowInvokeLink()
        {
            var findByPpsnDetailsRepr = _client.DomainServiceAction("sdm.restserver.RestRepositories.CustomerRepository", "FindByPPSN");

            var invokeLink = findByPpsnDetailsRepr.Links.Single(l => l.Rel.Contains("invoke"));
            var args = invokeLink.Arguments;
            args["PPSN"] = new ArgumentNodeForScalarRepr("0100303B");

            var actionResultRepr = invokeLink.Follow<GenericRepr>(_client, args).CastTo<ActionResultRepr>();
            actionResultRepr.ResultType.Should().Be("object");
            actionResultRepr.Result.Should().NotBeNull();

            var customerRepr = actionResultRepr.Result.CastTo<ObjectRepr>();
            customerRepr.DomainType.Should().Be("CUS");
            customerRepr.InstanceId.Should().Be("0100303B");
        }

    }
}
