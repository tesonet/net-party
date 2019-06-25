using System.Data.Linq.Mapping;

namespace NetPartyCore.Datastore.Model
{
    [Table(Name = "clients")]
    class Client
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public int ID { get; set; }

        [Column(Name = "username")]
        public string Username { get; set; }

        [Column(Name = "password")]
        public string Password { get; set; }
    }
}