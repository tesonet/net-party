using NetParty.Application.Interfaces;
using NetParty.Application.Server.Models;
using System.Collections.Generic;
using System.Text;

namespace NetParty.Application.Server.Commands.Models
{
    public class LocalServersDto : ICommandResult
    {
        public List<ServerDto> Servers { get; set; }
        public string GetText()
        {
            var serversViewInText = new StringBuilder();
            serversViewInText.AppendLine("||   Name    ||  Distance  ||");
            serversViewInText.AppendLine("-----------------------------");
            foreach (ServerDto server in Servers)
            {
                serversViewInText.AppendLine($"|| {server.Name} || {server.Distance} ||");
            }
            serversViewInText.AppendLine("-----------------------------");
 
            return serversViewInText.ToString();
        }
    }
}