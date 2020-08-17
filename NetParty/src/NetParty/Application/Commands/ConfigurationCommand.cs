using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using log4net;
using McMaster.Extensions.CommandLineUtils;
using NetParty.Domain.User;

namespace NetParty.Application.Commands
{
    public class ConfigurationCommand : ICommand
    {
        private readonly ILog _log = LogManager.GetLogger(nameof(ConfigurationCommand));

        private readonly ICredentialService _credentialService;

        public ConfigurationCommand(ICredentialService credentialService)
        {
            _credentialService = credentialService;
        }

        public void Attach(CommandLineApplication app)
        {
            CommandOption userName;
            CommandOption password;

            app.Command("config", cmd =>
            {
                cmd.Description = "Store username and password for API authorization";

                userName = cmd.Option("--username", "User name for API authorization", CommandOptionType.SingleValue)
                    .IsRequired();
                password = cmd.Option("--password", "Password for API authorization", CommandOptionType.SingleValue)
                    .IsRequired();

                cmd.ShowInHelpText = true;

                cmd.OnExecuteAsync(async cancelationToken =>
                {
                    _log.Debug("Updating credentials..");

                    var credentials = new Credentials(userName.Value(), password.Value());
                    var validation = await new CredentialsValidator().ValidateAsync(credentials, cancelationToken);

                    if (validation.IsValid)
                    {
                        await UpdateCredentials(credentials);

                        return 0;
                    }

                    DisplayValidationErrors(validation.Errors);

                    return 1;
                });
            });
        }

        private async Task UpdateCredentials(Credentials credentials)
        {
            Output.Info("Updating credentials..");
            try
            {
                await _credentialService.SaveAsync(credentials);

                Output.Info("Done!");
            }
            catch (Exception e)
            {
                Output.Error($"Couldn't update credentials due to: {e.Message}");
            }
        }

        private void DisplayValidationErrors(IEnumerable<ValidationFailure> errors)
        {
            _log.Debug("Invalid credentials were passed.");

            Output.Error("Invalid credentials were passed");

            foreach (var error in errors)
            {
                Output.Error($"! {error.PropertyName} : {error.ErrorMessage}");
            }
        }
    }
}