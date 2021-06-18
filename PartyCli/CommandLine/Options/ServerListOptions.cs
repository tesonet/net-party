using CommandLine;

namespace PartyCli.CommandLine.Options
{
	[Verb("server_list")]
	public record ServerListOptions
	{
		[Option("local", Default = false)] 
		public bool UseLocal { get; set; }
	}
}