using NetParty.Application.Tests.Infrastucture;
using NUnit.Framework;
using NetParty.Application.Server.Commands;

namespace NetParty.Application.Tests.Server.Commands
{
    [TestFixture]
    public class GetRemoteServersCommandTests
    {
        private GetRemoteServersCommand _getRemoteServersCommand;

        [SetUp]
        public void Setup()
        {
            _getRemoteServersCommand = new GetRemoteServersCommand();
        }

        [Test]
        public void TryParseParameters_Given_CommandParametersAsNull_Returns_True()
        {
            var executionResult = _getRemoteServersCommand.TryParseParameters(SubstitutedCommands.ServerListCommandParametersAsNull);
            Assert.IsTrue(executionResult);
        }

        [Test]
        public void TryParseParameters_Given_CommandParametersAsEmptyList_Returns_True()
        {
            var executionResult = _getRemoteServersCommand.TryParseParameters(SubstitutedCommands.ServerListCommandParametersAsEmptyList);
            Assert.IsTrue(executionResult);
        }

        [Test]
        public void TryParseParameters_Given_CommandParametersNotEmpty_Returns_False()
        {
            var executionResult = _getRemoteServersCommand.TryParseParameters(SubstitutedCommands.ServerListCommandWithParameters);
            Assert.IsFalse(executionResult);
        }

        [Test]
        public void TryParseParameters_Given_CommandAsNotServerList_Returns_False()
        {
            var executionResult = _getRemoteServersCommand.TryParseParameters(SubstitutedCommands.NotServerListCommandParametersAsNull);
            Assert.IsFalse(executionResult);
        }
    }
}
