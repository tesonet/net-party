namespace McMaster.Extensions.Hosting.CommandLine.Custom
{
    using CommandLineUtils;
    using CommandLineUtils.Abstractions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureCommandLineApplication<TApp>(
            this IHostBuilder hostBuilder,
            string[] args,
            out CommandLineState applicationState)
            where TApp : class
        {
            var state = new CommandLineState(args);

            hostBuilder.ConfigureServices((_, services) =>
            {
                services.AddSingleton<IUnhandledExceptionHandler, GlobalExceptionHandler>();
                services.AddSingleton(PhysicalConsole.Singleton);

                services
                    .AddSingleton<IHostLifetime, CommandLineLifetime>()
                    .AddSingleton(provider =>
                    {
                        state.SetConsole(provider.GetRequiredService<IConsole>());
                        return state;
                    })
                    .AddSingleton<CommandLineContext>(state)
                    .AddSingleton<ICommandLineService, CommandLineService<TApp>>();
            });

            applicationState = state;
            return hostBuilder;
        }
    }
}