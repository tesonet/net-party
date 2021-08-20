using CommandLine;
using partycli.Entities;
using partycli.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Any())
            {
                try
                {
                    ObjectContainer.Init();
                    Type[] types = { typeof(HelpOptions), typeof(UserOptions), typeof(ServerOptions) };
                    var parser = new Parser(a => a.CaseInsensitiveEnumValues = true);
                    parser.ParseArguments(args, types)
                        .WithParsed(Work)
                        .WithNotParsed(errs => ExitProgram("Arguments parser failure", true));
                }
                catch (Exception ex)
                {
                    ExitProgram(ex.Message, true);
                    return;
                }
            }
            else
            {
                ExitProgram("No arguments program will exit", false, true);
            }
        }

        public static void ExitProgram(string message, bool error = false, bool helpMessaage = false)
        {


            Console.BackgroundColor = error ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine($"{(error ? "[ERROR]" : "[SUCCESS]")} {message}");
            if (helpMessaage)
            {
                helpLines();
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }
        private static void Work(object obj)
        {
            var service = ObjectContainer.GetInstance<IService>();
            switch (obj)
            {
                case UserOptions userOptions:
                    {
                        var credentials = new Credentials
                        {
                            Username = userOptions.Username,
                            Password = userOptions.Password,
                        };
                        var result = service.SaveCredentials(credentials);
                        if (result.Success)
                            ExitProgram("Credentials were saved to persistent data.");
                        else
                            ExitProgram(result.Message, true);
                        return;
                    }
                case ServerOptions serverOptioins:
                    {
                        var args = new GetServersInfo
                        {
                            UseLocal = serverOptioins.Local
                        };

                        var result = service.GetServersList(args);
                        if (result.Success)
                            if (result.Servers?.Count > 0)
                                ServersListLines(result.Servers);
                            else
                                ExitProgram("Servers list is empty.");
                        else
                            ExitProgram(result.Message, true);

                        return;
                    }
                case HelpOptions help:
                case null: // in case somehow would go to default option
                    helpLines();
                    break;

            }
        }
        private static void helpLines()
        {
            /// Comment
            // Better to do from some config file
            // 
            ///
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("help info");
            Console.WriteLine("Commands:");
            Console.WriteLine("config --username \"YOUR USERNAME\" --password \"YOUR PASSWORD\"");
            Console.WriteLine("server_list");
            Console.WriteLine("server_list --local");
            Console.WriteLine("help info end");
        }
        private static void ServersListLines(List<ServerList> server)
        {
            Console.WriteLine($"Servers list amount: {server.Count}");
            Console.WriteLine("| {0,7} {1,26} {2, 7} |", "", "SERVERS LIST", "");
            Console.WriteLine("| {0,30} | {1,10} |", "SERVER NAME", "DISTANCE");

            foreach (var a in server)
            {
                if (a.Distance < 100)
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (a.Distance < 200)
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                else if (a.Distance < 300)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else if (a.Distance < 400)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                else if (a.Distance < 500)
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("| {0,30} | {1,10} | ", a.Name, a.Distance);
            }           
            ExitProgram(string.Empty);
        }
    }
}
