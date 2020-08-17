using NetParty.Domain.User;
using NUnit.Framework;

namespace NetParty.Tests.Domain.User
{
    [TestFixture]
    internal class CredentialsValidatorTests
    {
        private readonly CredentialsValidator _validator = new CredentialsValidator();

        [Test]
        [TestCase(null, "value", false)]
        [TestCase("", "value", false)]
        [TestCase("             ", "value", false)]
        [TestCase("value", null, false)]
        [TestCase("value", "", false)]
        [TestCase("value", "             ", false)]
        [TestCase("value", "value", true)]
        public void Validate_MustNotAllowEmptyValues(string userName, string password, bool success)
        {
            // act
            var result = _validator.Validate(new Credentials(userName, password));

            // very
            Assert.AreEqual(success, result.IsValid);
        }
    }
}
