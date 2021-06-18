using Microsoft.Extensions.DependencyInjection;

namespace PartyCli.Persistence.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPersistence(this IServiceCollection services) =>
			services
				.AddSingleton<IConfigRepository, FancyDatabaseImplementation>()
				.AddSingleton<IServerRepository, FancyDatabaseImplementation>();
	}
}