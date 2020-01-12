using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using net_party.Entities.Interfaces;
using net_party.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace net_party.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity, new()
    {
        protected readonly IServiceProvider _services;
        protected readonly ILogger _logger;
        protected readonly IConfigurationRoot _config;
        protected readonly SqlConnection _connection;

        protected BaseRepository(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
            _logger = _services.GetService<ILoggerFactory>().CreateLogger(GetType());
            _config = serviceProvider.GetService<IConfigurationRoot>();
            _connection = _services.GetService<SqlConnection>();
        }

        public async Task<long> Add(T entity)
        {
            if (_connection.State == ConnectionState.Broken || _connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();

            return await _connection.InsertAsync(entity);
        }

        public async Task<bool> Update(T entity)
        {
            if (_connection.State == ConnectionState.Broken || _connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();

            return await _connection.UpdateAsync(entity);
        }

        protected async Task<T> FirstOrNullAsync(string query)
        {
            if (_connection.State == ConnectionState.Broken || _connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();

            var result = await SqlMapper.QueryAsync<T>(_connection, query);

            return result.FirstOrDefault();
        }

        protected async Task<IEnumerable<T>> GetAsync(string query)
        {
            if (_connection.State == ConnectionState.Broken || _connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();
            
            return await SqlMapper.QueryAsync<T>(_connection, query);
        }
    }
}