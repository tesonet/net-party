using System;
using System.Reflection;
using CommandLine;
using Net_party.CommandLineControllers;
using Net_party.CommandLineModels;
using Net_party.Database;
using Net_party.Entities;
using Ninject;

namespace Net_party
{
    class Program
    {
        static void Main(string[] args)
        {

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var database = kernel.Get<IStorage>();

            args = new[] { "config", "--username", "Test", "--password", "test" };

            if (args.Length == 0)
            {
                Console.WriteLine("The application is supposed to be launched with arguments.");
                return;
            }


            switch (args[0].ToLower())
            {
                case "config":
                    Parser.Default.ParseArguments<CredentialsDto>(args)
                        .WithParsed(config =>
                        {
                            new CredentialsController(config);
                        });
                    break;
                case "server_list":
                    Parser.Default.ParseArguments<ServersRetrievalConfigurationDto>(args)
                        .WithParsed(o =>
                        {
                            Console.WriteLine(o.IsLocal);
                        });
                    break;
                default:
                    Console.WriteLine("Command not recognized");
                    break;
            }

        }
    }
}
