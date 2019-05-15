using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentValidation.Results;
using Moq;
using NetParty.Contracts;
using NetParty.Contracts.Exceptions;
using NetParty.Interfaces.Repositories;
using NetParty.Interfaces.Services;
using NetParty.Services;
using NUnit.Framework;

namespace NetParty.UnitTests
{
    [TestFixture]
    public class ConfigurationServiceTests
    {
        private ConfigurationService _service;
        private Mock<ICredentialsRepository> _credentialsRepositoryMock;
        private Mock<IValidationService> _validationServiceMock;
        private Mock<IOutputService> _outputServiceMock;
        private Fixture _fixture;

        [SetUp]
        public void Init()
        {
            _credentialsRepositoryMock = new Mock<ICredentialsRepository>();
            _validationServiceMock = new Mock<IValidationService>();
            _outputServiceMock = new Mock<IOutputService>();
            _fixture = new Fixture();
            _service = new ConfigurationService(_credentialsRepositoryMock.Object, _validationServiceMock.Object, _outputServiceMock.Object);
        }

        [Test]
        public void Given_NotValidCredentials_Expect_ValidationExceptionToBeThrown()
        {
            _validationServiceMock.Setup(x => x.Validate(It.IsAny<Credentials>()))
                .Returns(new ValidationResult(_fixture.Create<List<ValidationFailure>>()));

            Assert.ThrowsAsync<BaseValidationException>(async() => await _service.StoreCredentials(null));
            _credentialsRepositoryMock.Verify(x=> x.SaveCredentials(It.IsAny<Credentials>()), Times.Never);
        }

        [Test]
        public async Task Given_ValidCredentials_Expect_CredentialsToBeSaved()
        {
            _validationServiceMock.Setup(x => x.Validate(It.IsAny<Credentials>()))
                .Returns(new ValidationResult());

            await _service.StoreCredentials(null);
            _credentialsRepositoryMock.Verify(x => x.SaveCredentials(It.IsAny<Credentials>()), Times.Once);
        }
    }
}
