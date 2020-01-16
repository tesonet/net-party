using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using net_party.Entities.Interfaces;
using net_party.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace net_party.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity, new()
    {
        protected readonly IServiceProvider _services;
        protected readonly IConfigurationRoot _config;
        protected readonly SqlConnection _connection;

        protected BaseRepository(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
            _config = serviceProvider.GetService<IConfigurationRoot>();
            _connection = _services.GetService<SqlConnection>();

            _connection.Open();
        }

        public async Task<long> Add(T entity)
        {
            try
            {
                return await _connection.InsertAsync(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Command failed: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                return await _connection.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Command failed: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> Delete(T entity)
        {
            try
            {
                return await _connection.DeleteAsync(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Command failed: {ex.Message}");
                throw ex;
            }
        }

        protected async Task<T> FirstOrNullAsync(string query)
        {
            try
            {
                var result = await SqlMapper.QueryAsync<T>(_connection, query);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Command failed: {ex.Message}");
                throw ex;
            }
        }

        protected async Task<IEnumerable<T>> GetAsync(string query)
        {
            try
            {
                return await SqlMapper.QueryAsync<T>(_connection, query);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Command failed: {ex.Message}");
                throw ex;
            }
        }
    }
}