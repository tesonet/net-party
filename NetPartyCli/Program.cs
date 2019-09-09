using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CommandLine;
using NetPartyCli.Database;
using NetPartyCli.Dto;
using NetPartyCli.Presentation;
using NetPartyCli.Repositories;
using NetPartyCli.Services;

namespace NetPartyCli
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var services = LoadServices();
            var serviceProvider = services.BuildServiceProvider();

            var tesonetClient = serviceProvider.GetService<TesonetClient>();
            var serverService = serviceProvider.GetService<ServerService>();
            var userService = serviceProvider.GetService<UserService>();
            var display = serviceProvider.GetService<Display>();

            await Parser.Default.ParseArguments<UserOption, ServerOption>(args)
                .MapResult(
                    async (UserOption option) =>
                        await userService.AddAsync(new UserDto(option.Username, option.Password)),
                    async (ServerOption option) =>
                    {
                        var Allservers = await serverService.GetAllAsync(option.Local);
                        display.Show(Allservers);
                    },
                    er => Task.FromResult(0)
                );
            serviceProvider.Dispose();
        }
        public static IServiceCollection LoadServices()
        {
            var services = new ServiceCollection()
                .AddScoped<HttpClient, HttpClient>()
                .AddScoped<TesonetClient, TesonetClient>()
                .AddScoped<ServerRepository, ServerRepository>()
                .AddScoped<UserRepository, UserRepository>()
                .AddScoped<UserService, UserService>()
                .AddScoped<ServerService, ServerService>()
                .AddScoped<Display, Display>()
                .AddLogging(cfg => 
                {
                    cfg.AddConsole();
                    cfg.SetMinimumLevel(LogLevel.Warning);
                });
            var connection = @"Server=(localdb)\mssqllocaldb;Database=PartyDB";
            services.AddDbContext<PartyContext>(options => options.UseSqlServer(connection));
            return services;
        }
    }
}
