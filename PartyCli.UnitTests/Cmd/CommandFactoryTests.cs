using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using MediatR;
using Moq;
using PartyCli.CommandLine;
using PartyCli.CommandLine.Parsing;
using PartyCli.Contracts.Exceptions;
using PartyCli.Contracts.Response;
using TestUtils;
using Xunit;

namespace PartyCli.UnitTests.Cmd
{
	public class CommandFactoryTests
	{
		[Theory]
		[AutoMoqData]
		public void Throws_When_NoneOfTheParsersCanParseTheArguments([Frozen] List<Mock<ICommandParser<IRequest<ConsoleResponse>>>> parsers, string[] args)
		{
			foreach (var parser in parsers)
			{
				parser.Setup(p => p.Parse(args)).Returns((IRequest<ConsoleResponse>) null);
			}

			var c = new CommandFactory(parsers.Select(p => p.Object));
			Assert.Throws<PartyCliException>(() => c.Create(args));
		}

		[Theory]
		[AutoMoqData]
		public void ReturnsFirstParsedCommand([Frozen] List<Mock<ICommandParser<IRequest<ConsoleResponse>>>> parsers, string[] args, IRequest<ConsoleResponse> command)
		{
			foreach (var parser in parsers)
			{
				parser.Setup(p => p.Parse(args)).Returns((IRequest<ConsoleResponse>) null);
			}

			parsers.Skip(1).First().Setup(p => p.Parse(args)).Returns(command);

			var c = new CommandFactory(parsers.Select(p => p.Object));
			var actual = c.Create(args);

			Assert.Equal(command, actual);
		}
	}
}