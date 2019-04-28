using System;
using NetParty.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace NetParty.Application.CommandExecutor
{
    public class CommandFacade : ICommandFacade
    {
        public ICommand Create(ICommandArgs args)
        {
            foreach (var command in GetSupportedCommands())
            {
                if (command.TryParseParameters(args))
                {
                    command.SetParameters(args.CommandParameters);
                    return command;
                }
            };

            throw new Exception($"Command '{args.CommandName}' not supported!");
        }

        
        public IEnumerable<ICommand> GetSupportedCommands()
        {
            var commandInterface = typeof(ICommand);
            var allCommands = AppDomain.CurrentDomain.GetAssemblies()
              .SelectMany(assembly => assembly.GetTypes())
              .Where(typeFromAssembly => commandInterface.IsAssignableFrom(typeFromAssembly) && !typeFromAssembly.IsInterface && !typeFromAssembly.IsAbstract)
              .Select(commandType => (ICommand)Activator.CreateInstance(commandType));

            return allCommands;
        }
    }
}
