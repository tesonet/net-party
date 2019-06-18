using System.CommandLine;
using System.CommandLine.Invocation;

namespace NetPartyCore.Console
{
    interface IInputParser
    {
        Command CreateConfigCommand();

        Command CreateServerCommand();

        RootCommand CreateRootCommand();
    }
}
