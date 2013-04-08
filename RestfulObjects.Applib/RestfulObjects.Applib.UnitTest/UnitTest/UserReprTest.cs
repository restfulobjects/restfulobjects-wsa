using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestfulObjects.Applib.UnitTest
{
    [TestClass]
    public class UserReprTest
    {
        [TestMethod]
        public void As()
        {
            var userRepr = JsonRepr.FromResource<UserRepr>(Properties.Resources.User);
            userRepr.Extensions.Should().NotBeNull();
        }
    }
}
