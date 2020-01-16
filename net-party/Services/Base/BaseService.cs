﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using System;

namespace net_party.Services.Base
{
    public abstract class BaseService
    {
        protected readonly IServiceProvider _services;
        protected readonly IConfigurationRoot _config;
        protected readonly Logger _log;

        protected BaseService(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
            _config = serviceProvider.GetService<IConfigurationRoot>();
            _log = new LoggerConfiguration()
                .WriteTo.File($"log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}