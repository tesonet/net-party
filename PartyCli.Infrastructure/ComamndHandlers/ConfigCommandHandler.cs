using PartyCli.Core.Entities;
using PartyCli.Core.Enums;
using PartyCli.Core.Interfaces;
using PartyCli.Infrastructure.Exeptions;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PartyCli.Infrastructure.ComamndHandlers
{
    public class ConfigCommandHandler : ICommandHandler
    {
        private readonly IApiAuthCredentialsRepository _apiAuthCredentialsRepository;
        private readonly IPresenter _presenter;
        private readonly ILogger _logger;

        public AveilableCommands Command { get => AveilableCommands.config; }
            
        public ConfigCommandHandler(IApiAuthCredentialsRepository apiAuthCredentialsRepository, IPresenter presenter, ILogger logger)
        {
            _apiAuthCredentialsRepository = apiAuthCredentialsRepository;
            _presenter = presenter;
            _logger = logger;
        }

        public void Handle(string[] args)
        {
            if(!IsValidParams(args))
                throw new PresentableExeption("Invalid arguments.");

            var apiAuthCrediancials = ParseParams(args);

            _apiAuthCredentialsRepository.Add(apiAuthCrediancials);

            _logger.Debug("Configuration saved successfully.");
            _presenter.DisplayMessage("Configuration saved successfully.");
        }

        private bool IsValidParams(string[] args)
        {
            return args.Length == 5 &&
                    args[1] == "--username" &&
                    args[3] == "--password";
        }

        private ApiAuthCrediancials ParseParams(string[] args)
        {
            return new ApiAuthCrediancials
            {
                UserName = args[2],
                Password = args[4] 
            };
        }
    }
}
