using LiteDB;
using NetParty.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetParty.Model.Repositories
{
    public class Repository : IRepository
    {
        public Repository()
        {
        }

        public Credentials GetCredentials()
        {
            using (var db = new LiteDatabase("NetParty.db"))
            {
                var credentials = db.GetCollection<Credentials>("credentials");
                return credentials.FindAll().FirstOrDefault();
            }
        }

        public void SaveCredentials(Credentials args)
        {
            using (var db = new LiteDatabase("NetParty.db"))
            {
                if (db.CollectionExists("credentials"))
                {
                    var dropped = db.DropCollection("credentials");
                    if (!dropped)
                        throw new Exception("Delete old credentials failed.");
                }

                var credentials = db.GetCollection<Credentials>("credentials");
                credentials.Insert(args);
            }
        }

        public List<Server> GetServersList()
        {
            using (var db = new LiteDatabase("NetParty.db"))
            {
                var servers = db.GetCollection<Server>("servers");
                return servers.FindAll().ToList();
            }
        }

        public void SaveServerList(List<Server> list)
        {
            using (var db = new LiteDatabase("NetParty.db"))
            {
                if (db.CollectionExists("servers"))
                {
                    var dropped = db.DropCollection("servers");
                    if (!dropped)
                        throw new Exception("Delete old servers list failed.");
                }

                var servers = db.GetCollection<Server>("servers");
                servers.InsertBulk(list);
            }
        }
    }
}