using System;
using System.Threading.Tasks;
using CommandLine;
using partycli.Commands;
using Autofac;
namespace partycli
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ConfigCommand, ServerListCommand>(args)
                .WithParsed((c) => Task.Run(async () => await AbstractCommandHandler.StartWork((ICommand) c, ContainerConfig.Configure())));
            
            Console.Read();
        }
    }
}
