using McMaster.Extensions.CommandLineUtils;
using PartyCli.Interfaces;
using PartyCli.Models;
using System;

namespace PartyCli.Commands
{
    public class ConfigCommand
    {
        IRepository<Credentials> repository;

        CommandOption usernameOption;
        CommandOption passwordOption;

        public ConfigCommand(IRepository<Credentials> repository)
        {
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

            repository.Update(new[] { credentials });
            return 0;
        }
    }
}
