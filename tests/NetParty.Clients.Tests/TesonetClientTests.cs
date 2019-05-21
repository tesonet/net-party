using System;
using System.Configuration;
using NUnit.Framework;

namespace NetParty.Clients.Tests
{
    [TestFixture]
    public class TesonetClientTests
    {
        [Test]
        public void ConstructorNullReferenceExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => new TesonetClient(null));
        }

        [Test]
        public void GetTokenAsyncNullReferenceExceptionTest()
        {
            var client = new TesonetClient("http://google.com");

            Assert.ThrowsAsync<ArgumentException>(() => client.GetTokenAsync(null, "test"));
            Assert.ThrowsAsync<ArgumentException>(() => client.GetTokenAsync("test", null));
        }

        [Test]
        public void BadUserNameOrPasswordExceptionTest()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["tesonetApi"].ConnectionString;

            var client = new TesonetClient(connectionString);

            Assert.ThrowsAsync<Exception>(() => client.GetTokenAsync("test", "test"));
        }
    }
}
