using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;

namespace net_party.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IServiceProvider _services;
        protected readonly ILogger _logger;
        protected readonly IConfigurationRoot _config;
        protected readonly SqlConnection _connection;

        protected BaseRepository(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
            _logger = _services.GetService<ILoggerFactory>().CreateLogger(this.GetType());
            _config = serviceProvider.GetService<IConfigurationRoot>();
            _connection = _services.GetService<SqlConnection>();
        }
    }
}