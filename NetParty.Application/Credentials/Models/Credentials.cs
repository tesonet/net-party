using NetParty.Application.Interfaces;

namespace NetParty.Application.Credentials.Models
{
    public class Credentials : ICredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
