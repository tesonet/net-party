using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace NetPartyCore.Console
{
    class InputParser: IInputParser
    {
        public Command CreateConfigCommand()
        {
            return new Command("config", "Client configuration command", new List<Symbol>() {
                new Option("--username", "Api client username", new Argument<string>()),
                new Option("--password", "Api client password", new Argument<string>())
            }, null, true, CommandHandler.Create<string, string>((username, password) => {
                //Console.WriteLine($"The value for --username is: {username}");
                //Console.WriteLine($"The value for --password is: {password}");
            }), false);
        }

        
        public Command CreateServerCommand()
        {
            return new Command("server-list", "Server list management command", new List<Symbol>() {
                new Option("--local", "Display servers from local storage", new Argument<bool>()),
            }, null, true, CommandHandler.Create<bool>((local) => {
                //Console.WriteLine($"The value for --local is: {local}");
            }), false);
        }

        public RootCommand CreateRootCommand()
        {
            throw new NotImplementedException();
        }

    }
}
