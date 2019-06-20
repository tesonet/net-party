using System.ComponentModel.DataAnnotations.Schema;

namespace NetPartyCore.Datastore.Model
{
    class Server
    {

        public int ID { get; set; }

        public string Name { get; set; }

        public int Distance { get; set; }
    }

}
