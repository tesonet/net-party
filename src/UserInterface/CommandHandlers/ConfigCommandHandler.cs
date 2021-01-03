namespace Tesonet.ServerListApp.UserInterface.CommandHandlers
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Infrastructure.Configuration;
    using JetBrains.Annotations;
    using McMaster.Extensions.CommandLineUtils;
    using static HelpText;

    [Command(Name = "config", Description = Command.Config)]
    public class ConfigCommandHandler : BaseCommandHandler
    {
        private readonly IPersistentConfiguration _configuration;

        [Required]
        [Option("-u|--username <USERNAME>", Option.Username, CommandOptionType.SingleValue)]
        [UsedImplicitly]
        private string Username { get; } = string.Empty;

        [Required]
        [Option("-p|--password <PASSWORD>", Option.Password, CommandOptionType.SingleValue)]
        [UsedImplicitly]
        private string Password { get; } = string.Empty;

        public ConfigCommandHandler(IPersistentConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override Task OnExecuteAsync(CommandLineApplication app)
        {
            var configuration = new
            {
                ServerListApi = new
                {
                    Username,
                    Password,
                    BaseAddress = "https://playground.tesonet.lt/v1/"
                }
            };

            return _configuration.Save(configuration);
        }
    }
}