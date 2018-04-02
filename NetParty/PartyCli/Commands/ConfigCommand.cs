using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
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

        public ConfigCommand(ILogger logger, IRepository<Credentials> repository)
        {
            this.logger = logger?.ForContext<ConfigCommand>() ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public static void Configure(CommandLineApplication command, IServiceProvider services)
        {
            command.Description = "Provide username and password for API authorization";
            command.HelpOption();

            var usernameOption = command.Option("-u|--username <USERNAME>", "The username", CommandOptionType.SingleValue).IsRequired();
            var passwordOption = command.Option("-p|--password <PASSWORD>", "The password", CommandOptionType.SingleValue).IsRequired();

            command.OnExecute(() => services.GetService<ConfigCommand>().Execute(usernameOption.Value(), passwordOption.Value()));
        }
        
        private int Execute(string username, string password)
        {
            var credentials = new Credentials
            {
                Username = username,
                Password = password
            };

            logger.Debug("Saving API authorization credentials to persistent data store");
            repository.Update(new[] { credentials });

            logger.Information("API authorization credentials saved to persistent data store");
            return 0;
        }
    }
}
