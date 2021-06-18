using System;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PartyCli.Core.Api;
using PartyCli.Core.Behaviors;
using PartyCli.Core.Commands;
using PartyCli.Core.Options;
using PartyCli.Core.Services;

namespace PartyCli.Core.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
		{
			var apiSettings = new ApiSettings();
			configuration.GetSection("Api").Bind(apiSettings);

			services.AddTransient<IPlaygroundService, PlaygroundService>()
				.AddMediatR(typeof(GetHelpCommand).Assembly)
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>))
				.AddHttpClient<IPlaygroundApiClient, PlaygroundApiClient>(client =>
				{
					client.BaseAddress = new Uri(apiSettings.BaseUrl);
					client.Timeout = apiSettings.Timeout;
				});
			return services;
		}
	}
}