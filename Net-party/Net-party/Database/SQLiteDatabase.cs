using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;

namespace Net_party.Database
{
    class SqLiteDatabase : IStorage, IDisposable
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

        public DataContext GetContext()
        {
            return new DataContext(_connection);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
