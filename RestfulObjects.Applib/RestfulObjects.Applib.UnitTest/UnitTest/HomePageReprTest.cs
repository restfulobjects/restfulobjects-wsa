using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using NSubstitute;


namespace RestfulObjects.Applib.UnitTest
{
    [TestClass]
    public class HomePageRepresentationTest
    {
        private ROClient _client;
        private HomePageRepr homePageRepr;
        private ListRepr servicesRepr;
        private UserRepr userRepr;
        private VersionRepr versionRepr;

        [TestInitialize]
        public void SetUp()
        {
            homePageRepr = JsonRepr.FromResource<HomePageRepr>(Properties.Resources.HomePage);
            
            servicesRepr = JsonRepr.FromResource<ListRepr>(Properties.Resources.Services);
            userRepr = JsonRepr.FromResource<UserRepr>(Properties.Resources.User);
            versionRepr = JsonRepr.FromResource<VersionRepr>(Properties.Resources.Version);

            _client = Substitute.For<ROClient>();
            _client.HomePage().Returns(homePageRepr);
            _client.Services().Returns(servicesRepr);
            _client.User().Returns(userRepr);
            _client.Version().Returns(versionRepr);
        }

        [TestMethod]
        public void FollowSelf()
        {
            var selfLink = homePageRepr.Links.Single(l => l.Rel == "self");

            _client.Get<HomePageRepr>("http://localhost:7070/").Returns(homePageRepr);

            var followedLink = selfLink.Follow<HomePageRepr>(_client);

            followedLink.Links.Single(l => l.Rel == "self").Href.Should().Be(selfLink.Href);
        }

        [TestMethod]
        public void FollowUser()
        {
            var userLink = homePageRepr.Links.Single(l => l.Rel == "urn:org.restfulobjects:rels/user");

            _client.Get<UserRepr>("http://localhost:7070/user").Returns(userRepr);

            var followedLink = userLink.Follow<UserRepr>(_client);

            followedLink.Links.Single(l => l.Rel == "self").Href.Should().Be(userLink.Href);
        }

        [TestMethod]
        public void FollowVersion()
        {
            var versionLink = homePageRepr.Links.Single(l => l.Rel == "urn:org.restfulobjects:rels/version");

            _client.Get<VersionRepr>("http://localhost:7070/version").Returns(versionRepr);

            var followedLink = versionLink.Follow<VersionRepr>(_client);

            followedLink.Links.Single(l => l.Rel == "self").Href.Should().Be(versionLink.Href);
        }

        [TestMethod]
        public void FollowServices()
        {
            var servicesLink = homePageRepr.Links.Single(l => l.Rel == "urn:org.restfulobjects:rels/services");

            _client.Get<ListRepr>("http://localhost:7070/services").Returns(servicesRepr);

            var followedLink = servicesLink.Follow<ListRepr>(_client);

            followedLink.Links.Single(l => l.Rel == "self").Href.Should().Be(servicesLink.Href);
        }

        [TestMethod, Ignore]
        public void FollowDomainTypes()
        {
            var domainTypesLink = homePageRepr.Links.Single(l => l.Rel == "types");
            var followedLink = domainTypesLink.Follow<DomainTypesRepr>(_client);

            followedLink.Links.Single(l => l.Rel == "self").Href.Should().Be(domainTypesLink.Href);
        }


    }

}
