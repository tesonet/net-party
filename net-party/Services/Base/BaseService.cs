using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace net_party.Services.Base
{
    public abstract class BaseService
    {
        protected readonly IServiceProvider _services;
        protected readonly ILogger _logger;
        protected readonly IConfigurationRoot _config;

        protected BaseService(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
            _logger = _services.GetService<ILoggerFactory>().CreateLogger(this.GetType());
            _config = serviceProvider.GetService<IConfigurationRoot>();
        }
    }
}
