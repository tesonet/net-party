using CommandLine;
using NetParty.Common.Log;
using NetParty.Entities;
using NetParty.Model.Entities;
using NetParty.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetParty
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectContainer.Init();

            if (args != null && args.Any())
            {
                Logger.Debug("Program started. Args: {0}", string.Join(" ", args));
                try
                {
                    Type[] types = { typeof(ConfigOptions), typeof(ServerListOptions) };
                    var parser = new Parser(cfg => cfg.CaseInsensitiveEnumValues = true);
                    parser.ParseArguments(args, types)
                        .WithParsed(Run)
                        .WithNotParsed(errs => Exit("Parse arguments failed.", true));
                }
                catch (Exception ex)
                {
                    Exit(ex.Message, true);
                    return;
                }
            }
            else
            {
                Exit("No arguments.", true);
            }
        }

        private static void Run(object obj)
        {
            var service = ObjectContainer.GetInstance<IService>();

            switch (obj)
            {
                case ConfigOptions config:
                    {
                        var credentials = new Credentials
                        {
                            Username = config.Username,
                            Password = config.Password,
                        };
                        var result = service.SaveCredentials(credentials);
                        if (result.Success)
                            Exit("Credentials were saved to persistent data.");
                        else
                            Exit(result.Message, true);
                        return;
                    }
                case ServerListOptions serverList:
                    {
                        var args = new GetServersArgs
                        {
                            UseLocal = serverList.Local,
                            Order = serverList.Order, // Extra on servers_list: Add ordering by arguments "--order asc" or "--order desc"
                        };

                        var result = service.GetServersList(args);
                        if (result.Success)
                            if (result.Servers?.Count > 0)
                                PrintServersList(result.Servers);
                            else
                                Exit("Servers list is empty.");
                        else
                            Exit(result.Message, true);

                        return;
                    }
            }
        }

        private static void Exit(string message, bool onError = false)
        {
            Logger.Debug("Program Finished: onError {0}, Message: {1}", onError, message);
            Console.BackgroundColor = onError ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine($"{(onError ? "[ERROR]" : "[SUCCESS]")} {message}");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void PrintServersList(List<Server> servers)
        {
            Console.WriteLine("| {0,7} {1,26} {2, 7} |", "", "S E R V E R S       L I S T", "");
            Console.WriteLine("| {0,30} | {1,10} |", "SERVER NAME", "DISTANCE");

            foreach (var s in servers)
            {
                #region Extra: Color lines by distance
                if (s.Distance < 100)
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (s.Distance < 500)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Red;

                #endregion
                Console.WriteLine("| {0,30} | {1,10} | ", s.Name, s.Distance);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Total number of servers: {servers.Count}");
            Exit(string.Empty);
        }
    }
}