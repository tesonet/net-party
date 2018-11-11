using PartyCli.Core.Enums;
using PartyCli.Core.Interfaces;
using System;

namespace PartyCli.Infrastructure
{
    public class Receiver : IReceiver
    {
        private readonly Func<AveilableCommands, ICommandHandler> _commandHandlerFactory;
        private readonly ILogger _logger;

        public Receiver(Func<AveilableCommands, ICommandHandler> commandHandlerFactory, ILogger logger)
        {
            _commandHandlerFactory = commandHandlerFactory;
            _logger = logger;
        }

        public bool Accept(string[] args)
        {
            _logger.Debug($"Starting accept args: {String.Join(" ", args)}.");

            string commandString;

            if (args.Length < 1)
                commandString = AveilableCommands.unsuported.ToString();
            else
                commandString = args[0];

            if (Enum.TryParse<AveilableCommands>(commandString, out var command) == false)
            {
                command = AveilableCommands.unsuported;                
            }

            var comamndHandler = _commandHandlerFactory.Invoke(command);

            comamndHandler.Handle(args);

            _logger.Debug($"Finish accepting args: {String.Join(" ", args)}.");

            if (command == AveilableCommands.unsuported)
                return false;

            return true;
        }
    }
}
