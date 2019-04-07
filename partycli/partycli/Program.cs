using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using partycli.Commands;
using partycli.core;
using partycli.core.Execution;
using Autofac;
using partycli.core.DataAccess;
using partycli.Presentation;
using partycli.core.Logging;
using System.Reflection;

namespace partycli
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();

            ICommand command = null;
            Parser.Default.ParseArguments<ConfigCommand, ServerListCommand>(args)
                .WithParsed((c) => command = (ICommand)c);

            using (var scope = container.BeginLifetimeScope())
            {
                if (command != null)
                {
                    if (command.GetType() == typeof(ConfigCommand))
                    {
                        scope.Resolve<ICommandHandler<ConfigCommand>>().Handle((ConfigCommand)command);
                    }

                    if (command.GetType() == typeof(ServerListCommand))
                    {
                        scope.Resolve<ICommandHandler<ServerListCommand>>().Handle((ServerListCommand)command);
                    }
                }
            }
            
            Console.Read();
        }
    }
}
