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
            var firstCredentials = new Application.CredentialsNS.Credentials {Username = "TestUsername", Password = "TestPassword"};
            var secondCredentials = new Application.CredentialsNS.Credentials {Username = "TestUsername", Password = "TestPassword"};

            // act
            var areEqual = firstCredentials == secondCredentials;

            // assert
            areEqual.Should().BeTrue();
            }

        [Test]
        public void CredentialsAreDifferent_False()
            {
            // arrange
            var firstCredentials = new Application.CredentialsNS.Credentials {Username = "AAA", Password = "BBB"};
            var secondCredentials = new Application.CredentialsNS.Credentials {Username = "ZZZ", Password = "YYY"};

            // act
            var areEqual = firstCredentials == secondCredentials;

            // assert
            areEqual.Should().BeFalse();
            }
        }
    }
