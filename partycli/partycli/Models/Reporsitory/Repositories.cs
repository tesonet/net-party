using LiteDB;
using partycli.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Models.Reporsitory
{
    class Repositories : IRepository
    {
        public List<ServerList> GetServers()
        {
            using (var db = new LiteDatabase("Internal.db"))
            {
                var srv = db.GetCollection<ServerList>("ServerList");
                return srv.FindAll().ToList();
            }
        }

        public Credentials GetUserInfo()
        {
            using (var db = new LiteDatabase("Internal.db"))
            {
                var user = db.GetCollection<Credentials>("user");
                return user.FindAll().FirstOrDefault();
            }
        }

        public void SaveServers(List<ServerList> list)
        {
            using (var db = new LiteDatabase("Internal.db"))
            {
                if (db.CollectionExists("ServerList"))
                {
                    var drp = db.DropCollection("ServerList");
                    if (!drp)
                        throw new Exception("Failure droping old server list");
                }
                var srv = db.GetCollection<ServerList>("Internal");
                srv.InsertBulk(list);
            }
        }
        public void SaveUserInfo(Credentials args)
        {
            using (var db = new LiteDatabase("Internal.db"))
            {
                if (db.CollectionExists("user"))
                {
                    var dropped = db.DropCollection("user");
                    if (!dropped)
                        throw new Exception("Failure droping old user");
                }
                var credentials = db.GetCollection<Credentials>("user");
                credentials.Insert(args);
            }
        }
    }
}
