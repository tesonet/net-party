using McMaster.Extensions.CommandLineUtils;
using PartyCli.Interfaces;
using PartyCli.Models;
using Serilog;
using System;

namespace PartyCli.Commands
{
    public class ConfigCommand
    {
        ILogger logger;
        IRepository<Credentials> repository;

        CommandOption usernameOption;
        CommandOption passwordOption;

        public ConfigCommand(ILogger logger, IRepository<Credentials> repository)
        {
            this.logger = logger?.ForContext<ConfigCommand>() ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Configure(CommandLineApplication command)
        {
            command.Description = "Provide username and password for API authorization";
            command.HelpOption();

            usernameOption = command.Option("-u|--username <USERNAME>", "The username", CommandOptionType.SingleValue).IsRequired();
            passwordOption = command.Option("-p|--password <PASSWORD>", "The password", CommandOptionType.SingleValue).IsRequired();

            command.OnExecute(() => Execute());
        }
        
        private int Execute()
        {
            var credentials = new Credentials
            {
                Username = usernameOption.Value(),
                Password = passwordOption.Value()
            };

            logger.Debug("Saving API authorization credentials to persistent data store");
            repository.Update(new[] { credentials });

            logger.Information("API authorization credentials saved to persistent data store");
            return 0;
        }
    }
}
