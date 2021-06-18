using System.Threading.Tasks;
using CommandLine;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PartyCli.CommandLine;
using PartyCli.CommandLine.Mapping;
using PartyCli.CommandLine.Options;
using PartyCli.CommandLine.Parsing;
using PartyCli.Contracts.Response;
using PartyCli.Core.Commands;
using PartyCli.Core.Extensions;
using PartyCli.Output;
using PartyCli.Persistence.Extensions;

namespace PartyCli
{
	internal static class Program
	{
		private static async Task Main(string[] args)
		{
			using var host = CreateHost(args);
			await host.StartAsync();
		}

		private static IHost CreateHost(string[] args)
		{
			var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

			return Host.CreateDefaultBuilder(args).ConfigureServices(services =>
			{
				services.AddLogging(builder =>
					{
						builder.ClearProviders();
						builder.AddConsole(); // Kind of pointless for a CLI app but...
					})
					.AddMediatR(typeof(Program).Assembly)
					.AddCoreServices(configuration)
					.AddParsing(args)
					.AddPersistence()
					.AddAutoMapper(expression => expression.AddProfile(new CommandMapperProfile()))
					.AddHostedService<PartyCliHostedService>();
			}).UseConsoleLifetime().Build();
		}

		private static IServiceCollection AddParsing(this IServiceCollection services, string[] args) =>
			services.AddSingleton<ICommandLineArgumentsAccessor>(_ => new CommandLineArgumentsAccessor(args))
				.AddSingleton(_ => new Parser(settings => { settings.AutoHelp = false; }))
				.AddTransient<IConsoleOutputWriter, ConsoleOutputWriter>()
				.AddTransient<ICommandFactory, CommandFactory>()
				.AddCommands();

		private static IServiceCollection AddCommands(this IServiceCollection services) =>
			services
				.AddTransient<ICommandParser<IRequest<ConsoleResponse>>, CommandParser<ConfigOptions, SaveConfigCommand>>()
				.AddTransient<ICommandParser<IRequest<ConsoleResponse>>, CommandParser<ServerListOptions, GetServerListCommand>>()
				.AddTransient<ICommandParser<IRequest<ConsoleResponse>>, CommandParser<HelpOptions, GetHelpCommand>>();
	}
}