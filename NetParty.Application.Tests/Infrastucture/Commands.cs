using NetParty.Application.Constants;
using System.Collections.Generic;

namespace NetParty.Application.Tests.Infrastucture
{
    public static class SubstitutedCommands
    {
        public static SubCommand NotServerListCommandParametersAsNull = new SubCommand()
        {
            CommandName = "not server list command",
            CommandParameters = null
        };
        public static SubCommand ServerListCommandParametersAsNull = new SubCommand()
        {
            CommandName = Command.GetServers,
            CommandParameters = null
        };
        public static SubCommand ServerListCommandParametersAsEmptyList = new SubCommand()
        {
            CommandName = Command.GetServers,
            CommandParameters = new Dictionary<string, string>()
        };
        public static SubCommand ServerListCommandWithParameters = new SubCommand()
        {
            CommandName = Command.GetServers,
            CommandParameters = new Dictionary<string, string>() { { "key1", "value1" } }
        };

        public static SubCommand ServerListCommandWithFlagParameterAndValue = new SubCommand()
        {
            CommandName = Command.GetServers,
            CommandParameters = new Dictionary<string, string>() { { Parameter.FlagToFetchOnlyPersistedServers, "value1" } }
        };

        public static SubCommand ServerListCommandWithFlagParameterWithoutValue = new SubCommand()
        {
            CommandName = Command.GetServers,
            CommandParameters = new Dictionary<string, string>() { { Parameter.FlagToFetchOnlyPersistedServers, null } }
        };

    }
}
