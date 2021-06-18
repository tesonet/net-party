using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using PartyCli.Contracts.Exceptions;
using PartyCli.Contracts.Models;

namespace PartyCli.Persistence
{
	public class FancyDatabaseImplementation : IConfigRepository, IServerRepository
	{
		private const string ConfigDbPath = "config.db";
		private const string ServerDbPath = "servers.db";

		public async Task Save(Config config) => await Write(config, ConfigDbPath);

		public Task<Config> GetConfig() => Read<Config>(ConfigDbPath);

		public async Task Save(IEnumerable<Server> servers) => await Write(servers, ServerDbPath);

		public Task<IEnumerable<Server>> GetServers() => Read<IEnumerable<Server>>(ServerDbPath);

		private static async Task<T> Read<T>(string path)
		{
			if (!File.Exists(path))
			{
				throw new PartyCliException("Could not find database file");
			}

			try
			{
				await using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
				return await JsonSerializer.DeserializeAsync<T>(stream);
			}
			catch (Exception)
			{
				throw new PartyCliException("Error reading data");
			}
		}

		private static async Task Write<T>(T item, string path)
		{
			File.Delete(path);
			await using var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
			await JsonSerializer.SerializeAsync(stream, item);
		}
	}
}