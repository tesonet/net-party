using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Dapper;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using net_party.Repositories;
using net_party.Repositories.Contracts;
using net_party.Repositories.Sql;
using net_party.Services;
using net_party.Services.Contracts;
using Serilog;
using Serilog.Core;

namespace net_party
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            var services = new ServiceCollection()
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
                    .AddJsonFile("appsettings.json", true)
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

            ExecuteApplication(args, serviceProvider);
        }

        public static void ExecuteApplication(string[] args, ServiceProvider services)
        {
             var log = new LoggerConfiguration()
                .WriteTo.File($"log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            log.Information($"{DateTime.UtcNow}: Application launched.");

            var app = new CommandLineApplication
            {
                Name = "net-party",
                Description = "Application for getting a list of servers."
            };
            app.HelpOption("-?|-h|--help");

            app.OnExecute(() =>
            {
                Console.WriteLine("Specify --help for a list of available options and commands.");

                return 0;
            });

            #region server_list

            app.Command("server_list", command =>
            {
                command.Description = "Get a list of servers.";
                command.HelpOption("-?|-h|--help");

                var local = command.Option("-l|--local",
                    "Flag the operation to get servers from local storage.",
                    CommandOptionType.NoValue);

                command.OnExecute(async () =>
                {
                    log.Information($"{DateTime.UtcNow}: \"server_list\" executed.");
                    Console.WriteLine("Searching for servers...");

                    var isLocal = local.HasValue();

                    var serverService = services.GetService<IServerService>();
                    var serverList = await serverService.GetServers(isLocal);

                    Console.WriteLine("Servers:");

                    var serverCount = 0;
                    foreach (var server in serverList)
                    {
                        Console.WriteLine($"Server - {server.Name}");
                        Console.WriteLine($"Distance - {server.Distance}");
                        
                        serverCount++;
                    }

                    Console.WriteLine($"Total servers found: {serverCount}");

                    log.Information($"{DateTime.UtcNow}: \"server_list\" execution finished successfully.");

                    return 0;
                });
            });

            #endregion server_list

            #region config

            app.Command("config", command =>
            {
                command.Description = "Set the user and password used for authentication.";
                command.HelpOption("-?|-h|--help");

                var usernameOption = command.Option("--username <value>",
                    "Username used for authentication.",
                    CommandOptionType.SingleValue);

                var passwordOption = command.Option("--password <value>", "Password used for authentication", CommandOptionType.SingleValue);

                command.OnExecute(async () =>
                {
                    log.Information($"{DateTime.UtcNow}: \"config\" execution begun.");

                    string username = usernameOption.Value();
                    string password = passwordOption.Value();

                    if (!usernameOption.HasValue())
                    {
                        Console.WriteLine("Invalid argument: username");
                        var ex = new ArgumentException();
                        log.Error(ex, $"{DateTime.UtcNow}: Application encountered an error.");
                    }

                    if (!passwordOption.HasValue())
                    {
                        Console.WriteLine("Invalid argument: password");
                        var ex = new ArgumentException();
                        log.Error(ex, $"{DateTime.UtcNow}: Application encountered an error.");
                    }

                    var authService = services.GetService<IAuthService>();
                    await authService.AuthenticateCredentials(username, password);

                    log.Information($"{DateTime.UtcNow}: \"config\" execution finished successfully.");

                    return 0;
                });
            });

            #endregion config

            #region setup_db

            app.Command("setup_db", command =>
            {
                command.Description = "Runs a script on the connected database to create the required tables for persistent storage.";
                command.HelpOption("-?|-h|--help");

                command.OnExecute(async () =>
                {
                    log.Information($"{DateTime.UtcNow}: setup_db executed.");
                    Console.WriteLine("Beginning database table setup...");

                    var connection = services.GetService<SqlConnection>();
                    await connection.OpenAsync();
                    await connection.ExecuteAsync(TablesSql.CreateTablesSql);

                    Console.WriteLine("Database table setup has finished!");
                    return 0;
                });
            });

            #endregion setup_db

            try
            {
                app.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to execute application: {ex.Message}");

                log.Error(ex, $"{DateTime.UtcNow}: Application exited with error.");
            }
        }

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            var log = new LoggerConfiguration()
                .WriteTo.File($"log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            log.Error($"{DateTime.UtcNow}: Unhandled exception encoutered. {e.ExceptionObject.ToString()}");

            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}