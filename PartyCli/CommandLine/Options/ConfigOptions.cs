using CommandLine;

namespace PartyCli.CommandLine.Options
{
	[Verb("config")]
	public record ConfigOptions
	{
		[Option("username")] 
		public string UserName { get; set; }

		[Option("password")] 
		public string Password { get; set; }
	}
}