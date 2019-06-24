using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetPartyCore.Datastore.Model;
using System.IO;
using System.Data.SQLite;

namespace NetPartyCore.Datastore
{
    class SQLiteStorage : IStorage
    {
        public SQLiteStorage()
        {

            if (!File.Exists("partycli.db"))
            {
                PrepareDatabase();
            }
        }

        public Client GetConfiguration()
        {
            var connection = new SQLiteConnection("Data Source=partycli.db;Version=3;");
            Client client;
            connection.Open();
            using (var reader = new SQLiteCommand(@"SELECT * FROM clients LIMIT 1", connection).ExecuteReader())
            {
                reader.Read();
                client = new Client(reader.GetString(1), reader.GetString(2));   
            }

            connection.Close();
            return client;
        }

        public List<Server> GetServers()
        {
            var connection = new SQLiteConnection("Data Source=partycli.db;Version=3;");
            connection.Open();

            var servers = new List<Server>();
            using (var reader = new SQLiteCommand(@"SELECT * FROM servers", connection).ExecuteReader())
            {
                while (reader.Read())
                {
                    servers.Add(new Server(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                }
            }

            connection.Close();
            return servers;
        }

        public void SetConfiguration(Client client)
        {

            var connection = new SQLiteConnection("Data Source=partycli.db;Version=3;");
            connection.Open();

            var insertClientCommand = new SQLiteCommand($"INSERT INTO clients (username, password) VALUES ('{client.Username}', '{client.Password}')", connection);
            var updateClinetCOmmand = new SQLiteCommand($"UPDATE clients SET username = '{client.Username}', password = '{client.Password}' WHERE id = 1", connection);

            using (var reader = new SQLiteCommand(@"SELECT * FROM clients LIMIT 1", connection).ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    insertClientCommand.ExecuteNonQuery();
                } else
                {
                    updateClinetCOmmand.ExecuteNonQuery();
                }
            }

            connection.Close();
        }

        public void SetSevers(List<Server> servers)
        {
            var connection = new SQLiteConnection("Data Source=partycli.db;Version=3;");
            connection.Open();

            var clearServersCommand = new SQLiteCommand("DELETE FROM servers", connection);
            
            clearServersCommand.ExecuteNonQuery();

            foreach (Server server in servers)
            {
                var insertClientCommand = new SQLiteCommand($"INSERT INTO servers (name, distance) VALUES ('{server.Name}', '{server.Distance}')", connection);
                insertClientCommand.ExecuteNonQuery();
            }

            connection.Close();
        }

        private void PrepareDatabase()
        {
            SQLiteConnection.CreateFile("partycli.db");
            var connection = new SQLiteConnection("Data Source=partycli.db;Version=3;");
            connection.Open();
            

            var createClientsTableCommand = new SQLiteCommand("CREATE TABLE clients (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, username TEXT NOT NULL, password TEXT NOT NULL)", connection);
            createClientsTableCommand.ExecuteNonQuery();

            var createServersTableCommand = new SQLiteCommand("CREATE TABLE servers (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, distance INTEGER NOT NULL)", connection);
            createServersTableCommand.ExecuteNonQuery();

            connection.Close();
        }
    }
}
