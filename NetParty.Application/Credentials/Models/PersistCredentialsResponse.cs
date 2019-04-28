using NetParty.Application.Interfaces;
using System;

namespace NetParty.Application.Credentials.Commands
{
    public class PersistCredentialsResponse : ICommandResult
    {
        public string GetText()
        {
            return "OK";
        }
    }
}
