using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace net_party.Services.Base
{
    public abstract class BaseService
    {
        protected readonly IServiceProvider _services;
        protected readonly IConfigurationRoot _config;

        protected BaseService(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
            _config = serviceProvider.GetService<IConfigurationRoot>();
        }
    }
}