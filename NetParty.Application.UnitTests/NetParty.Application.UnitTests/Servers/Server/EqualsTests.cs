#region Using

using FluentAssertions;
using NUnit.Framework;

#endregion

namespace NetParty.Application.UnitTests.Servers.Server
    {
    [TestFixture]
    public class EqualsTests
        {
        [Test]
        public void CredentialsAreEqual_True()
            {
            // arrange
            var firstServer = new Application.Servers.Server("AAA", 100);
            var secondServer = new Application.Servers.Server("AAA", 100);

            // act
            var areEqual = firstServer == secondServer;

            // assert
            areEqual.Should().BeTrue();
            }

        [Test]
        public void CredentialsAreDifferent_False()
            {
            // arrange
            var firstServer = new Application.Servers.Server("AAA", 100);
            var secondServer = new Application.Servers.Server("ZZZ", 555);

            // act
            var areEqual = firstServer == secondServer;

            // assert
            areEqual.Should().BeFalse();
            }
        }
    }
