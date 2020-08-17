using System.Collections.Generic;
using McMaster.Extensions.CommandLineUtils;
using NetParty.Application.Commands;

namespace NetParty.Application
{
    internal class CommandBuilder
    {
        public static CommandLineApplication Build(IEnumerable<ICommand> commands)
        {
            var app = new CommandLineApplication
            {
                Name = "partycli",
                Description = "App for showing and saving servers received from API",
            };

            app.HelpOption("-h|--help");

            foreach (var command in commands)
            {
                command.Attach(app);
            }

            app.OnExecute(() =>
            {
                Output.Error("You must specify a command");

                app.ShowHelp();

                return 1;
            });


            return app;
        }
    }
}
