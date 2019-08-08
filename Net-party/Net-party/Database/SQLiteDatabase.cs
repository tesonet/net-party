using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Net_party.CommandLineModels;
using Net_party.Entities;

namespace Net_party.Database
{
    class SqLiteDatabase : IStorage
    {
        private const string DatabaseName = "NetParty.db";

        private SQLiteConnection _connection;

        public SqLiteDatabase()
        {
            if (!File.Exists(DatabaseName))
            {
                Init();
                return;
            }

            _connection = new SQLiteConnection($"Data Source={DatabaseName};Version=3;");
        }


        public async Task SaveUser(CredentialsDto userConfig)
        {
            var context = new DataContext(_connection);
            context.GetTable<CredentialsDto>().InsertOnSubmit(userConfig);
            context.SubmitChanges();
        }

        public CredentTaskalsDto GetUser()
        {
            throw new NotImplementedException();
        }


        public Task SaveServers(IEnumerable<ServersRetrievalConfigurationDto> servers)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Server>> GetServers()
        {
            throw new NotImplementedException();
        }

        private void Init()
        {
            SQLiteConnection.CreateFile(DatabaseName);
            _connection = new SQLiteConnection($"Data Source={DatabaseName};Version=3;");
            _connection.Open();

            var serversListTableCreationSql = "CREATE TABLE Servers (" +
                                              "Id INTEGER PRIMARY KEY," +
                                              "ServerName TEXT NOT NULL," +
                                              "Distance INTEGER NOT NULL)";

            new SQLiteCommand(serversListTableCreationSql, _connection).ExecuteNonQuery();

            var userConfigurationTableCreationSql = "CREATE TABLE UsersConfiguration (" +
                                              "Id INTEGER PRIMARY KEY," +
                                              "Username TEXT NOT NULL," +
                                              "Password TEXT NOT NULL)";

            new SQLiteCommand(userConfigurationTableCreationSql, _connection).ExecuteNonQuery();

        }
    }
}
