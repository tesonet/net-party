using System.Data.Linq.Mapping;

namespace Net_party.Entities
{
    [Table(Name = "Servers")]
    public class Server
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int? Id { get; set; }

        [Column(Name = "ServerName")]
        public string Name { get; set; }

        [Column(Name = "Distance")]
        public int Distance { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Distance)}: {Distance}";
        }
    }
}
