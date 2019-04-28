using MediatR;
using NetParty.Application.Constants;
using NetParty.Application.Interfaces;
using System.Collections.Generic;

namespace NetParty.Application.Server.Commands
{
    public class FetchPersistedServersCommand : IRequest<ICommandResult>, ICommand
    {
        public void SetParameters(IDictionary<string, string> commandParameters)
        {

        }

        public bool TryParseParameters(ICommandArgs args)
        {
            return args != null &&
               args.CommandParameters != null &&
               args.CommandName == Command.GetServers &&
               args.CommandParameters.Count == 1 &&
               args.CommandParameters.Keys.Contains(Parameter.FlagToFetchOnlyPersistedServers) &&
               args.CommandParameters[Parameter.FlagToFetchOnlyPersistedServers] == null;
        }
    }
}