using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Reflection;

namespace NetPartyCore.Framework
{
    /**
     * A helper class to simplify route adding from main Program
     */
    class CommandRouter
    {
        private RootCommand rootCommand = new RootCommand();

        public CommandRouter()
        {
            rootCommand.Description = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyDescriptionAttribute>()
                .Description.ToString();

            rootCommand.TreatUnmatchedTokensAsErrors = true;
        }

        public CommandRouter AddRoute(string name, string desription, IReadOnlyCollection<Option> options, ICommandHandler handler)
        {
            rootCommand.AddCommand(new Command(name, desription, options, null, true, handler, false));
            return this;
        }

        public RootCommand GetRootCommand()
        {
            return rootCommand;
        }

    }
}
