using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PartyCli.Contracts.Models;
using PartyCli.Contracts.Response;
using PartyCli.Core.Commands;
using PartyCli.Core.Services;
using PartyCli.Persistence;

namespace PartyCli.Core.CommandHandlers
{
	public class GetServerListCommandHandler : IRequestHandler<GetServerListCommand, ConsoleResponse>
	{
		private readonly IPlaygroundService _playgroundService;
		private readonly IConfigRepository _configRepository;
		private readonly IServerRepository _serverRepository;

		public GetServerListCommandHandler(IPlaygroundService playgroundService, IConfigRepository configRepository, IServerRepository serverRepository)
		{
			_playgroundService = playgroundService;
			_configRepository = configRepository;
			_serverRepository = serverRepository;
		}

		public async Task<ConsoleResponse> Handle(GetServerListCommand request, CancellationToken cancellationToken)
		{
			ICollection<Server> servers;
			if (request.UseLocal)
			{
				servers = (await _serverRepository.GetServers()).ToList();
			}
			else
			{
				var userConfig = await _configRepository.GetConfig();
				servers = (await _playgroundService.GetServers(userConfig)).ToList();
				await _serverRepository.Save(servers);
			}

			return CreateResponse(servers);
		}

		private static ConsoleResponse CreateResponse(IEnumerable<Server> servers)
		{
			var response = new ConsoleResponse();

			foreach (var (name, distance) in servers)
			{
				response.Lines.Add(new ConsoleLine($"Name: {name}, Distance: {distance}"));
			}

			response.Lines.Add(new ConsoleLine($"Total {response.Lines.Count} servers"));

			return response;
		}
	}
}