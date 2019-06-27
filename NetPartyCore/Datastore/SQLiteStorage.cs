using System.Collections.Generic;
using System.Linq;
using NetPartyCore.Datastore.Model;
using System.IO;
using System.Data.SQLite;
using System.Data.Linq;

namespace NetPartyCore.Datastore
{
    class SQLiteStorage : IStorage
    {
        private SQLiteConnection connection;

        public SQLiteStorage()
        {
            var databseExists = File.Exists("partycli.db");

            if (!databseExists)
            {
                SQLiteConnection.CreateFile("partycli.db");
            }

            connection = new SQLiteConnection("Data Source=partycli.db;Version=3;");

            if (!databseExists)
            {
                connection.Open();
                var createClientsTableCommand = new SQLiteCommand("CREATE TABLE clients (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, username TEXT NOT NULL, password TEXT NOT NULL)", connection);
                createClientsTableCommand.ExecuteNonQuery();

                var createServersTableCommand = new SQLiteCommand("CREATE TABLE servers (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, distance INTEGER NOT NULL)", connection);
                createServersTableCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        private DataContext GetContext()
        {
            return new DataContext(connection);
        }

        public Client GetConfiguration()
        {
            using (DataContext dc = GetContext())
            {
                var table = dc.GetTable<Client>();
                return table.SingleOrDefault();
            }
        }

        public void SetConfiguration(Client client)
        {
            using (DataContext dc = GetContext()) {
                var table = dc.GetTable<Client>();
                var current = table.SingleOrDefault();

                if (current != null)
                {
                    current.Username = client.Username;
                    current.Password = client.Password;
                }
                else
                {
                    table.InsertOnSubmit(client);
                }

                dc.SubmitChanges();
            }
        }

        public List<Server> GetServers()
        {
            using (DataContext dc = GetContext())
            {
                var table = dc.GetTable<Server>();
                var rows = from item in table select item;
                return rows.ToList();
            }
        }

        public void SetSevers(List<Server> servers)
        {
            // had to resort using plain connection instead of linq because of a following bug in SQL linq library:
            // https://stackoverflow.com/questions/18677411/wrong-sql-statements-being-generated-when-using-system-data-sqlite-linq
            connection.Open();

            new SQLiteCommand("DELETE FROM servers", connection)
                .ExecuteNonQuery();

            servers.ForEach(server => {
                new SQLiteCommand($"INSERT INTO servers (name, distance) VALUES ('{server.Name}', '{server.Distance}')", connection)
                    .ExecuteNonQuery();
            });

            connection.Close();
        }
    }
}
