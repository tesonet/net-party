using System.Collections.Generic;
using System.Threading.Tasks;
using NetParty.Contracts;
using NetParty.Interfaces.Repositories;
using SQLite;

namespace NetParty.Repositories
{
    public class ServersRepository : IServersRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public ServersRepository(string databasePath)
        {
            _connection = new SQLiteAsyncConnection(databasePath);
        }

        public Task<List<Server>> GetServers()
        {
            return _connection.Table<Server>().ToListAsync();
        }

        public async Task SaveServers(List<Server> servers)
        {
            await _connection.DropTableAsync<Server>();
            await _connection.CreateTableAsync<Server>();
            await _connection.InsertAllAsync(servers);
        }
    }
}