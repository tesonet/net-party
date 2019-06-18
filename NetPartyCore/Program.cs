using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Reflection;
using System.Threading.Tasks;

namespace NetPartyCore
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            try
            {


                /*var configCommand = new Command("config", "Client configuration command", new List<Symbol>() {
                    new Option("--username", "Api client username", new Argument<string>()),
                    new Option("--password", "Api client password", new Argument<string>())
                }, null, true, CommandHandler.Create<string, string>((username, password) => {
                    Console.WriteLine($"The value for --username is: {username}");
                    Console.WriteLine($"The value for --password is: {password}");
                }), false);

                var serverCommand = new Command("server-list", "Server list management command", new List<Symbol>() {
                    new Option("--local", "Display servers from local storage", new Argument<bool>()),
                }, null, true, CommandHandler.Create<bool>((local) => {
                    Console.WriteLine($"The value for --local is: {local}");
                }), false);*/
                //configCommand, serverCommand
                var commands = new List<Symbol>() {  };

                // get application descriptiom from asemly information
                var description = Assembly.GetExecutingAssembly()
                    .GetCustomAttribute<AssemblyDescriptionAttribute>()
                    .Description.ToString();

                // Parse the incoming args and invoke the handler
                return await new RootCommand(description, commands, null, true, null, false)
                    .InvokeAsync(args);
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.ToString());
                System.Console.ReadKey();
                return 0;
            }
        }
    }
}
