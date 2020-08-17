using System.Text;
using System.Threading.Tasks;
using Moq;
using NetParty.Data;
using NetParty.Domain.User;
using Newtonsoft.Json;
using NUnit.Framework;

namespace NetParty.Tests.Domain.User
{
    [TestFixture]
    internal class EncryptedCredentialServiceTest
    {
        private Mock<IRepository<byte[]>> _credentialsRepoMock;
        private EncryptedCredentialService _service;

        [SetUp]
        public void SetUp()
        {
            _credentialsRepoMock = new Mock<IRepository<byte[]>>();
            _service = new EncryptedCredentialService(_credentialsRepoMock.Object);
        }

        [Test]
        public async Task SaveAsync_ShouldEncryptValues()
        {
            // arrange
            var initialValue = new Credentials("usr", "pwd");
            var savedValue = JsonConvert.SerializeObject(initialValue);
            _credentialsRepoMock.Setup(_ => _.SaveAsync(It.IsAny<byte[]>())).Callback<byte[]>(val =>
            {
                savedValue = Encoding.UTF8.GetString(val);
            });

            // act
            await _service.SaveAsync(initialValue);

            // verify
            Assert.AreNotEqual(savedValue, initialValue);
            Assert.Throws<JsonReaderException>(() => JsonConvert.DeserializeObject<Credentials>(savedValue));
        }

        [Test]
        public async Task GetAsync_ShouldDecryptValues()
        {
            // arrange
            var expected = new Credentials("user", "password");
            var cred = new byte[0];

            _credentialsRepoMock.Setup(_ => _.SaveAsync(It.IsAny<byte[]>()))
                .Callback<byte[]>(value =>
                {
                    cred = value;
                });

            await _service.SaveAsync(expected);

            _credentialsRepoMock.Setup(_ => _.GetAsync()).ReturnsAsync(cred);


            // act
            var result = await _service.GetAsync();

            // verify
            Assert.AreEqual(expected.UserName, result.UserName);
            Assert.AreEqual(expected.Password, result.Password);
        }
    }
}
