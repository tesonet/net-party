namespace Tesonet.ServerListApp.UserInterface.CommandHandlers
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Infrastructure.ServerListApi;
    using Infrastructure.Storage;
    using JetBrains.Annotations;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.EntityFrameworkCore;

    [Command(Name = "config", Description = "Set API credentials")]
    public class ConfigCommandHandler : BaseCommandHandler
    {
        private readonly ServersDbContext _dbContext;

        [Required]
        [Option("-u|--username <USERNAME>", "Username for API authorization", CommandOptionType.SingleValue)]
        [UsedImplicitly]
        private string Username { get; } = string.Empty;

        [Required]
        [Option("-p|--password <PASSWORD>", "Password for API authorization", CommandOptionType.SingleValue)]
        [UsedImplicitly]
        private string Password { get; } = string.Empty;

        public ConfigCommandHandler(ServersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task OnExecuteAsync(CommandLineApplication app)
        {
            var credentials = await _dbContext.ClientCredentials.SingleOrDefaultAsync();

            if (credentials != null)
            {
                _dbContext.Remove(credentials);
                await _dbContext.SaveChangesAsync();
            }

            await _dbContext.AddAsync(new ClientCredentials(Username, Password));
            await _dbContext.SaveChangesAsync();
        }
    }
}