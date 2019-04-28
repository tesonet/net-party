using MediatR;
using NetParty.Application.Constants;
using NetParty.Application.Interfaces;
using System.Collections.Generic;

namespace NetParty.Application.Credentials.Commands
{
    public class PersistCredentialsCommand: IRequest<ICommandResult>, ICommand
    {
        public Models.Credentials Credentials { get; set; }

        public bool TryParseParameters(ICommandArgs args)
        {
            return args != null &&
                args.CommandParameters != null &&
                args.CommandName == Command.SetCredentials &&
                args.CommandParameters.Count == 2 &&
                args.CommandParameters.Keys.Contains(Parameter.UserName) &&
                args.CommandParameters.Keys.Contains(Parameter.Password);
        }
        public void SetParameters(IDictionary<string, string> commandParameters)
        {
            Credentials = new Models.Credentials
            {
                Username = commandParameters[Parameter.UserName],
                Password = commandParameters[Parameter.Password]
            };
        }
    }
}
