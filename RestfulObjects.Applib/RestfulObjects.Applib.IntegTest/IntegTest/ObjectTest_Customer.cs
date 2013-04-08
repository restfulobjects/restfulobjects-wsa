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
    public class ObjectTest_Customer
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
            var customerRepr = _client.DomainObject("Sdm.Cluster.Customers.Impl.Customer", "59");
            customerRepr.Should().NotBeNull();
            customerRepr.DomainType.Should().Be("Sdm.Cluster.Customers.Impl.Customer");
            customerRepr.InstanceId.Should().Be("59");
        }


    }
}
