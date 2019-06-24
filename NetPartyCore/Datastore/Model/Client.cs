using System.ComponentModel.DataAnnotations.Schema;

namespace NetPartyCore.Datastore.Model
{
    class Client
    {
        public Client(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
