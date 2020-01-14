using Dapper;
using Dapper.Contrib.Extensions;
using net_party.Entities.Database;
using net_party.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace net_party.Repositories
{
    public class ServerRepository : BaseRepository<Server>, IServerRepository
    {
        public ServerRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<IEnumerable<Server>> Get()
        {
            if (_connection.State == ConnectionState.Broken || _connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();

            var sql = $"SELECT * FROM {nameof(Server)}s";
            return await GetAsync(sql);
        }

        public async Task Truncate(IDbTransaction transaction = null)
        {
            var sql = $"TRUNCATE TABLE {nameof(Server)}s";
            await SqlMapper.ExecuteAsync(_connection, sql, transaction);
        }

        public async Task<long> AddMany(IEnumerable<Server> entity, IDbTransaction transaction = null)
        {
            long insertedRows;
            insertedRows = await _connection.InsertAsync(entity, transaction);

            return insertedRows;
        }
    }
}