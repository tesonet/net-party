using CommandLine;

namespace Tesonet.NetParty.Models.Models
{
	public class ServerListCommandArgs
	{
		[Option("local", Required = false)]
		public bool IsLocal { get; set; }
	}
}
