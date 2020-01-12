using Dapper.Contrib.Extensions;
using net_party.Entities.Interfaces;

namespace net_party.Entities.Database
{
    public class Credential : IEntity
    {
        [Key]
        public long Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
