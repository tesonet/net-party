using PartyCli.Core.Entities;
using PartyCli.Core.Enums;
using PartyCli.Core.Interfaces;
using PartyCli.Infrastructure.Exeptions;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PartyCli.Infrastructure.ComamndHandlers
{
    public class ServerListCommandHandler : ICommandHandler
    {
        private readonly IServersApi _serversApi;
        private readonly IServersRepository _serversRepository;
        private readonly IPresenter _presenter;
        private readonly ILogger _logger;

        public AveilableCommands Command { get => AveilableCommands.server_list; }

        public ServerListCommandHandler(IServersApi serversApi, 
                                        IServersRepository serversRepository, 
                                        IPresenter presenter,
                                        ILogger logger)
        {
            _serversApi = serversApi;
            _serversRepository = serversRepository;
            _presenter = presenter;
            _logger = logger;
        }

        public void Handle(string[] args)
        {
            if (!IsValidParams(args))
                throw new PresentableExeption("Invalid arguments.");

            var serversList = new List<Server>();

            if (args.Length == 1) // if more possible params strategy could be  implemented
            {
                serversList = _serversApi.GetServersAsync().Result;
                _serversRepository.AddRange(serversList);
            }
            else
            {
                serversList = _serversRepository.GetAll().ToList();

                if (serversList.Any() == false)
                {
                    _presenter.DisplayMessage("No servers found. Try to load from api.");
                    return;
                }

            }

            _presenter.DisplayServers(serversList);

            _logger.Debug("Server list presented.");
        }

        private bool IsValidParams(string[] args)
        {
            return args.Length == 1 ||
                   (args.Length == 2 && args[1] != "--local");
        }
    }
}
