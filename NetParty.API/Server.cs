using NetParty.Application.Interfaces;

namespace NetParty.Api
{
    internal class Server : IServer
    {
        public string Name { get; set; }
        public string Distance { get; set; }
    }
}