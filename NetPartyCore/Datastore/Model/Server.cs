using System.Data.Linq.Mapping;

namespace NetPartyCore.Datastore.Model
{
    [Table (Name = "servers")]
    class Server
    {
        [Column (Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column (Name = "name")]
        public string Name { get; set; }

        [Column(Name = "distance")]
        public int Distance { get; set; }
    }

}
