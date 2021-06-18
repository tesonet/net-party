using AutoMapper;
using PartyCli.CommandLine.Options;
using PartyCli.Core.Commands;

namespace PartyCli.CommandLine.Mapping
{
	public class CommandMapperProfile : Profile
	{
		public CommandMapperProfile()
		{
			CreateMap<ConfigOptions, SaveConfigCommand>();
			CreateMap<ServerListOptions, GetServerListCommand>();
			CreateMap<HelpOptions, GetHelpCommand>();
		}
	}
}