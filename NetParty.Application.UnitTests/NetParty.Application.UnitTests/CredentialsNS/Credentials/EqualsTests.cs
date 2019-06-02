#region Using

using FluentAssertions;
using NUnit.Framework;

#endregion

namespace NetParty.Application.UnitTests.CredentialsNS.Credentials
    {
    [TestFixture]
    public class EqualsTests
        {
        [Test]
        public void CredentialsAreEqual_True()
            {
            // arrange
            var firstCredentials = new Application.CredentialsNS.Credentials("TestUsername", "TestPassword");
            var secondCredentials = new Application.CredentialsNS.Credentials("TestUsername", "TestPassword");

            // act
            var areEqual = firstCredentials == secondCredentials;

            // assert
            areEqual.Should().BeTrue();
            }

        [Test]
        public void CredentialsAreDifferent_False()
            {
            // arrange
            var firstCredentials = new Application.CredentialsNS.Credentials("AAA", "BBB");
            var secondCredentials = new Application.CredentialsNS.Credentials("ZZZ", "YYY");

            // act
            var areEqual = firstCredentials == secondCredentials;

            // assert
            areEqual.Should().BeFalse();
            }
        }
    }
