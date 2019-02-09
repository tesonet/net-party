using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Castle.Core.Logging;
using PartyCli.Entities;
using PartyCli.Repositories;

namespace PartyCli.UnitTests
{
  [TestFixture]
  public class ServicesTests
  {
    private Mock<ILogger> _loggerMock;
    private Mock<IRepository<Credentials>> _credentialsRepository;

    [SetUp]
    public void Setup()
    {
      _loggerMock = new Mock<ILogger>();      
    }

    [Test]
    public async Task Local_Service_Should_Load_Servers()
    {
      // TODO: test implementation
    }

    [Test]
    public async Task Remote_Service_Should_Load_Servers()
    {
      // TODO: test implementation
    }
  }
}
