using System.ComponentModel.DataAnnotations.Schema;

namespace NetPartyCore.Datastore.Model
{
    class Server
    {

        public Server(int ID, string name, int distance)
        {
            this.ID = ID;
            this.Name = name;
            this.Distance = distance;
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public int Distance { get; set; }
    }

}
