using NetParty.Application.Tests.Infrastucture;
using NUnit.Framework;
using NetParty.Application.Constants;
using NetParty.Application.Server.Commands;
using System.Collections.Generic;

namespace NetParty.Application.Tests.Server.Commands
{
    [TestFixture]
    public class FetchPersistedServersCommandTests
    {
        private FetchPersistedServersCommand _fetchPersistedServersCommand;
        
        [SetUp]
        public void Setup()
        {
            _fetchPersistedServersCommand = new FetchPersistedServersCommand();
        }

        [Test]
        public void TryParseParameters_Given_CommandParametersAsNull_Returns_False()
        {
            var executionResult = _fetchPersistedServersCommand.TryParseParameters(SubstitutedCommands.ServerListCommandParametersAsNull);
            Assert.IsFalse(executionResult);
        }

        [Test]
        public void TryParseParameters_Given_CommandParametersAsEmptyList_Returns_False()
        {
            var executionResult = _fetchPersistedServersCommand.TryParseParameters(SubstitutedCommands.ServerListCommandParametersAsEmptyList);
            Assert.IsFalse(executionResult);
        }

        [Test]
        public void TryParseParameters_Given_AnyCommandParameters_Returns_False()
        {
            var executionResult = _fetchPersistedServersCommand.TryParseParameters(SubstitutedCommands.ServerListCommandWithParameters);
            Assert.IsFalse(executionResult);
        }

        [Test]
        public void TryParseParameters_Given_CommandAsNotServerList_Returns_False()
        {
            var executionResult = _fetchPersistedServersCommand.TryParseParameters(SubstitutedCommands.NotServerListCommandParametersAsNull);
            Assert.IsFalse(executionResult);
        }

        [Test]
        public void TryParseParameters_Given_FlagCommandParameterWithValue_Returns_False()
        {
            var executionResult = _fetchPersistedServersCommand.TryParseParameters(SubstitutedCommands.ServerListCommandWithFlagParameterAndValue);
            Assert.IsFalse(executionResult);
        }

        [Test]
        public void TryParseParameters_Given_FlagCommandParameterWithoutValue_Returns_True()
        {
            var executionResult = _fetchPersistedServersCommand.TryParseParameters(SubstitutedCommands.ServerListCommandWithFlagParameterWithoutValue);
            Assert.IsTrue(executionResult);
        }
    }
}
