using Dapper.Contrib.Extensions;
using net_party.Entities.API;
using net_party.Entities.Interfaces;

namespace net_party.Entities.Database
{
    public class Server : IEntity
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
        public long Distance { get; set; }

        public Server() { }

        public static Server FromResponse(ServerListResponse response)
        {
            return new Server()
            {
                Name = response.Name,
                Distance = response.Distance
            };
        }
    }
}
