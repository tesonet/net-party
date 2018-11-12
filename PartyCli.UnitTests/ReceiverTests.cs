using Moq;
using PartyCli.Core.Enums;
using PartyCli.Core.Interfaces;
using PartyCli.Infrastructure;
using System;
using Xunit;

namespace PartyCli.UnitTests
{
    public class ReceiverTests
    {
        [Theory]
        [InlineData("config")]
        [InlineData("server_list")]
        public void Receiver_IsAccepting(string command)
        {
            var mockCommandhandler = new Mock<ICommandHandler>();

            Func<AveilableCommands, ICommandHandler> commandFactory = cmd => { return mockCommandhandler.Object; };

            var mockLogger = new Mock<ILogger>();

            var receiver = new Receiver(commandFactory, mockLogger.Object);

            var result = receiver.Accept(new string[] { command });

            Assert.True(result);
        }

        [Fact]
        public void Receiver_IsFalingNoParams()
        {
            var mockCommandhandler = new Mock<ICommandHandler>();

            Func<AveilableCommands, ICommandHandler> commandFactory = cmd => { return mockCommandhandler.Object; };

            var mockLogger = new Mock<ILogger>();

            var receiver = new Receiver(commandFactory, mockLogger.Object);

            var result = receiver.Accept(new string[] { });

            Assert.False(result);

        }

        [Fact]
        public void Receiver_IsFalingWithUnsuported()
        {
            var mockCommandhandler = new Mock<ICommandHandler>();

            Func<AveilableCommands, ICommandHandler> commandFactory = cmd => { return mockCommandhandler.Object; };

            var mockLogger = new Mock<ILogger>();

            var receiver = new Receiver(commandFactory, mockLogger.Object);

            var result = receiver.Accept(new string[] { "usup" });

            Assert.False(result);

        }
    }
}
