using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestfulObjects.Applib.UnitTest
{
    public class AdhocGenericRepr : JsonRepr
    {
        public GenericRepr UserName { get; set; }
        public List<GenericRepr> Roles { get; set; }

        public GenericRepr SomeNumber { get; set; }

        public List<GenericRepr> Links { get; set; }
        public GenericRepr Extensions { get; set; }
    }

    [TestClass]
    public class AdhocGenericReprTest
    {
        [TestMethod]
        public void As()
        {
            var repr = JsonRepr.FromResource<AdhocGenericRepr>(Properties.Resources.User);

            repr.UserName.Should().NotBeNull();
            var username = repr.UserName;
            var scalarRepr = username.CastTo<ScalarRepr>();
            scalarRepr.AsString().Should().Be("WELFARE\\sven");

            repr.Roles.Should().NotBeNull();
            repr.Roles.Count.Should().Be(3);
            var role0 = repr.Roles[0].CastTo<ScalarRepr>();
            role0.AsString().Should().Be("role1");


            repr.Links.Should().NotBeNull();
            repr.Links.Count.Should().Be(2);
            var linkRepr = repr.Links[0].CastTo<LinkRepr>();

            linkRepr.Rel.Should().Be("self");

            repr.Extensions.Should().NotBeNull();
        }
    }


}
