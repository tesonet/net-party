using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using MediatR;
using Moq;
using PartyCli.CommandHandlers;
using PartyCli.CommandLine;
using PartyCli.Commands;
using PartyCli.Contracts.Response;
using TestUtils;
using Xunit;

namespace PartyCli.UnitTests.CommandHandlers
{
	public class ParseCommandLineCommandHandlerTests
	{
		[Theory]
		[AutoMoqData]
		public async Task ReturnsParsedCommand(
			[Frozen] Mock<ICommandLineArgumentsAccessor> commandLineArgumentsAccessor,
			[Frozen] Mock<ICommandFactory> commandFactory,
			string[] args,
			IRequest<ConsoleResponse> parsedCommand,
			ParseCommandLineCommand command,
			ParseCommandLineCommandHandler handler)
		{
			commandLineArgumentsAccessor.Setup(c => c.Arguments).Returns(args);

			commandFactory.Setup(c => c.Create(args)).Returns(parsedCommand);

			var actual = await handler.Handle(command, CancellationToken.None);

			Assert.Equal(parsedCommand, actual);
		}
	}
}