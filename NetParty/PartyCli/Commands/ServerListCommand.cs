using McMaster.Extensions.CommandLineUtils;
using PartyCli.Interfaces;
using PartyCli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyCli.Commands
{
    public class ServerListCommand
    {
        IServerApi serversApi;
        IRepository<Server> serversRepository;
        IRepository<Credentials> credentialsRepository;
        IServerPresenter serversPresenter;

        CommandOption localOption;

        public ServerListCommand(IServerApi serversApi, IRepository<Server> serversRepository, IRepository<Credentials> credentialsRepository, IServerPresenter serversPresenter)
        {
            this.serversApi = serversApi ?? throw new ArgumentNullException(nameof(serversApi)); 
            this.serversRepository = serversRepository ?? throw new ArgumentNullException(nameof(serversRepository));
            this.credentialsRepository = credentialsRepository ?? throw new ArgumentNullException(nameof(credentialsRepository));
            this.serversPresenter = serversPresenter ?? throw new ArgumentNullException(nameof(serversPresenter));
        }

        public void Configure(CommandLineApplication command)
        {
            command.Description = "Fetch servers from API and display them";
            command.HelpOption();

            localOption = command.Option("-l|--local", "Use previously fetched data", CommandOptionType.NoValue);

            command.OnExecute(async () => await Execute());
        }
        
        private async Task<int> Execute()
        {
            IEnumerable<Server> servers;
            if (localOption.HasValue())
            {
                servers = GetLocal();
            }
            else
            {
                servers = await GetFromApi();
            }

            serversPresenter.Display(servers);

            return 0;
        }

        private IEnumerable<Server> GetLocal()
        {
            return serversRepository.GetAll();
        }

        private async Task<IEnumerable<Server>> GetFromApi()
        {
            var apiCredentials = credentialsRepository.GetAll().FirstOrDefault();

            if (apiCredentials == null)
            {
                throw new Exception("No API credentials specified!\n\r" +
                    "Use config command to provide username and password for API authorization.");
            }

            await serversApi.AuthorizeAsync(apiCredentials);
            var servers = await serversApi.GetServersAsync();
            serversRepository.Update(servers);

            return servers;
        }
    }
}
