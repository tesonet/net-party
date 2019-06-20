using System.ComponentModel.DataAnnotations.Schema;

namespace NetPartyCore.Datastore.Model
{
    class Client
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
