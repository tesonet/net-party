namespace Tesonet.ServerListApp.UserInterface.CommandHandlers
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using McMaster.Extensions.CommandLineUtils;

    [Command(Name = "partycli.exe", Description = "Tesonet .NET party application")]
    [Subcommand(typeof(ConfigCommandHandler), typeof(ServerListCommandHandler))]
    [UsedImplicitly]
    public class RootCommandHandler : BaseCommandHandler
    {
        protected override Task OnExecuteAsync(CommandLineApplication app)
        {
            app.ShowHelp();
            return Task.CompletedTask;
        }
    }
}