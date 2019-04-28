using System.Collections.Generic;
using MediatR;
using NetParty.Application.Constants;
using NetParty.Application.Interfaces;

namespace NetParty.Application.Server.Commands
{
    public class GetRemoteServersCommand : IRequest<ICommandResult>, ICommand
    {
        public void SetParameters(IDictionary<string, string> commandParameters)
        {
            
        }

        public bool TryParseParameters(ICommandArgs args)
        {
            return args != null &&
               (args.CommandParameters == null || args.CommandParameters.Count == 0) &&
               args.CommandName == Command.GetServers;
        }
    }
}