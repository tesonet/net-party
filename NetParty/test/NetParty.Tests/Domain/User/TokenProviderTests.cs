using System;
using System.Net.Http;
using Moq;
using NetParty.Domain.User;
using NUnit.Framework;

namespace NetParty.Tests.Domain.User
{
    [TestFixture]
    internal class TokenProviderTests : IDisposable
    {
        private Mock<ICredentialService> _credentialServiceMock;
        private TokenProvider _tokenProvider;
        private HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            _credentialServiceMock = new Mock<ICredentialService>();
            _httpClient = new HttpClient();
            _tokenProvider = new TokenProvider(_httpClient, "", _credentialServiceMock.Object);
        }

        [Test]
        public void GetToken_ShouldThrowException_IfNoCredentialsAreAvailable()
        {
            Assert.ThrowsAsync<Exception>(async () => await _tokenProvider.GetToken());
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
