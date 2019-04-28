using NetParty.Application.Interfaces;

namespace NetParty.Application.Server.Models
{
    public class ServerDto: IServer
    {
        public string Name { get; set; }
        public string Distance { get; set; }
    }
}
