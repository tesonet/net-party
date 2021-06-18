using System.Collections.Generic;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using PartyCli.CommandLine.Options;
using PartyCli.CommandLine.Parsing;
using PartyCli.Core.Commands;
using TestUtils;
using Xunit;

namespace PartyCli.UnitTests.Cmd
{
	public class CommandParserTests
	{
		[Theory]
		[AutoMoqData]
		public void ParsesGetHelpOption([Frozen] Mock<IMapper> mapper, CommandParser<HelpOptions, GetHelpCommand> parser)
		{
			var args = new[] { "help" };

			var command = parser.Parse(args);

			Assert.NotNull(command);
			mapper.Verify(m => m.Map<GetHelpCommand>(It.IsAny<HelpOptions>()), Times.Once);
		}

		[Theory]
		[AutoMoqData]
		public void ParsesGetServerListOption([Frozen] Mock<IMapper> mapper, CommandParser<ServerListOptions, GetServerListCommand> parser, bool isLocal)
		{
			mapper.Setup(m => m.Map<object>(It.IsAny<object>())).Returns(new Fixture().Create<GetServerListCommand>());
			var args = new List<string>() { "server_list" };
			if (isLocal)
			{
				args.Add("--local");
			}

			var command = parser.Parse(args);

			Assert.NotNull(command);
			mapper.Verify(m => m.Map<GetServerListCommand>(It.Is<ServerListOptions>(o => o.UseLocal == isLocal)), Times.Once);
		}

		[Theory]
		[AutoMoqData]
		public void ParsesConfigOption([Frozen] Mock<IMapper> mapper, CommandParser<ConfigOptions, SaveConfigCommand> parser, string userName, string password)
		{
			mapper.Setup(m => m.Map<object>(It.IsAny<object>())).Returns(new Fixture().Create<SaveConfigCommand>());

			var args = new[] { "config", "--username", userName, "--password", password };

			var command = parser.Parse(args);

			Assert.NotNull(command);
			mapper.Verify(m => m.Map<SaveConfigCommand>(It.Is<ConfigOptions>(o => o.Password == password && o.UserName == userName)), Times.Once);
		}

		[Theory]
		[AutoMoqData]
		public void ReturnsNull_When_ArgsAreNotRecognized(CommandParser<ConfigOptions, SaveConfigCommand> parser)
		{
			var args = new[] { "random_stuff" };

			var command = parser.Parse(args);

			Assert.Null(command);
		}
	}
}