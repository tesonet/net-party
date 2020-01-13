using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using net_party.Repositories;
using net_party.Repositories.Contracts;
using net_party.Services;
using net_party.Services.Contracts;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace net_party
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddLogging()

                // Repositories
                .AddScoped<IAuthTokenRepository, AuthTokenRepository>()
                .AddScoped<ICredentialRepository, CredentialRepository>()
                .AddScoped<IServerRepository, ServerRepository>()

                // Services
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IServerService, ServerService>()
                .AddScoped<IPasswordService, PasswordService>()
                .AddSingleton<IRngService, RngService>()
                .AddSingleton<RNGCryptoServiceProvider>()
                .AddSingleton(provider => new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build());

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var config = scope.ServiceProvider.GetService<IConfigurationRoot>();

                services.AddTransient(v =>
                {
                    return new SqlConnection(config["ConnectionStrings:DefaultConnection"]);
                });
            }

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Starting application");

            ExecuteApplication(args, serviceProvider);

            Console.ReadLine();

            logger.LogDebug("Application done!");
        }

        private static void ExecuteApplication(string[] args, ServiceProvider services)
        {
            var logger = services.GetService<ILoggerFactory>().CreateLogger("Debug");

            var app = new CommandLineApplication(throwOnUnexpectedArg: true);
            app.Name = "net-party";
            app.Description = "Application for getting a list of servers.";
            app.HelpOption("-?|-h|--help");

            app.Command("server_list", (command) =>
            {
                Console.WriteLine("server_list");
                command.Description = "Get a list of servers.";
                command.HelpOption("-?|-h|--help");

                var local = command.Option("-l|--local",
                    "Flag the operation to get servers from local storage.",
                    CommandOptionType.NoValue);

                command.OnExecute(async () =>
                {
                    logger.LogInformation("server_list is executing");

                    bool isLocal = local.HasValue();

                    var serverService = services.GetService<IServerService>();
                    var serverList = await serverService.GetServers(isLocal);

                    foreach(var server in serverList)
                    {
                        Console.WriteLine($"Server: {server.Name}");
                        Console.WriteLine($"Distance: {server.Distance}");
                    }

                    logger.LogInformation("server_list has finished");
                    return 0;
                });
            });

            app.Command("config", (command) =>
            {
                command.Description = "Set the user and password used for authentication.";
                command.HelpOption("-?|-h|--help");

                var usernameOption = command.Option("--username <value>",
                    "Username used for authentication.",
                    CommandOptionType.SingleValue);

                var passwordOption = command.Option("--password <password>", "Password used for authentication", CommandOptionType.SingleValue);

                command.OnExecute(async () =>
                {
                    logger.LogInformation("config is executing");

                    string username = usernameOption.Value();
                    string password = passwordOption.Value();

                    var authService = services.GetService<IAuthService>();
                    //authService. TODO Set credentials;
                    Console.WriteLine("executing config");
                    logger.LogInformation("config has finished");
                    return 0;
                });
            });

            try
            {
                logger.LogInformation("Application has begun executing...");
                app.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogInformation("Unable to execute application: {0}", ex.Message);
            }
        }
    }
}