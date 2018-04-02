using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using PartyCli.Interfaces;
using PartyCli.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyCli.Commands
{
    public class ServerListCommand
    {
        ILogger logger;
        IServerApi serversApi;
        IRepository<Server> serversRepository;
        IRepository<Credentials> credentialsRepository;
        IServerPresenter serversPresenter;

        public ServerListCommand(ILogger logger, IServerApi serversApi, IRepository<Server> serversRepository, IRepository<Credentials> credentialsRepository, IServerPresenter serversPresenter)
        {
            this.logger = logger?.ForContext<ServerListCommand>() ?? throw new ArgumentNullException(nameof(logger));
            this.serversApi = serversApi ?? throw new ArgumentNullException(nameof(serversApi)); 
            this.serversRepository = serversRepository ?? throw new ArgumentNullException(nameof(serversRepository));
            this.credentialsRepository = credentialsRepository ?? throw new ArgumentNullException(nameof(credentialsRepository));
            this.serversPresenter = serversPresenter ?? throw new ArgumentNullException(nameof(serversPresenter));
        }

        public static void Configure(CommandLineApplication command, IServiceProvider services)
        {
            command.Description = "Fetch servers from API and display them";
            command.HelpOption();

            var localOption = command.Option("-l|--local", "Use previously fetched data", CommandOptionType.NoValue);

            command.OnExecute(async () => await services.GetService<ServerListCommand>().Execute(localOption.HasValue()));
        }
        
        private async Task<int> Execute(bool local)
        {
            IEnumerable<Server> servers;
            if (local)
            {
                servers = GetLocal();
            }
            else
            {
                servers = await GetFromApi();
            }

            logger.Debug("Displaying servers");
            serversPresenter.Display(servers);

            return 0;
        }

        private IEnumerable<Server> GetLocal()
        {
            logger.Debug("Retrieving servers from persistent data store");
            var servers = serversRepository.GetAll();
            logger.Information("{NumberOfServers} servers retrieved from persistent data store", servers.Count());

            return servers;
        }

        private async Task<IEnumerable<Server>> GetFromApi()
        {
            logger.Debug("Retrieving API credentials from persistent data store");
            var apiCredentials = credentialsRepository.GetAll().FirstOrDefault();

            if (apiCredentials == null)
            {
                throw new Exception("No API credentials specified!\n\r" +
                    "Use config command to provide username and password for API authorization.");
            }

            logger.Debug("Authorizing to API");
            await serversApi.AuthorizeAsync(apiCredentials);

            logger.Debug("Retrieving servers from API");
            var servers = await serversApi.GetServersAsync();
            logger.Information("{NumberOfServers} servers retrieved from API", servers.Count());

            logger.Debug("Saving servers to persistent data store");
            serversRepository.Update(servers);

            return servers;
        }
    }
}
