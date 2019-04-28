using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using NetParty.Application.Credentials.Commands;
using NetParty.Application.Interfaces;
using NetParty.Application.Server.Commands;

namespace NetParty.Application.CommandExecutor
{
    public class CommandsExecutor : ICommandsExecutor
    {
        private ICommandFacade _commandFacade;
        private IMediator _mediator;

        public CommandsExecutor(ICommandFacade commandFacade, IMediator mediator)
        {
            _commandFacade = commandFacade;
            _mediator = mediator;

        }
        public async Task<IEnumerable<ICommandResult>> Execute(IEnumerable<ICommandArgs> commandsWithArgs)
        {
            var results = new List<ICommandResult>();

            foreach (var commandArgs in commandsWithArgs)
            {
                var command = _commandFacade.Create(commandArgs);
                var singleCommandResult = await Execute(command);
                results.Add(singleCommandResult);
            }

            return results;
        }

        // TODO: make it work...
        private async Task<ICommandResult> Execute(ICommand command)
        {
            var commandTypeName = command.GetType().Name;

            switch (commandTypeName)
            {
                case "PersistCredentialsCommand":
                    return await _mediator.Send((PersistCredentialsCommand)command);
                case "GetRemoteServersCommand":
                    return await _mediator.Send((GetRemoteServersCommand)command);
                case "FetchPersistedServersCommand":
                    return await _mediator.Send((FetchPersistedServersCommand)command);
                default:
                    throw new System.Exception($"Command type {commandTypeName} not found!");

            }
        }

    }
}
