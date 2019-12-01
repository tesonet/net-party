using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tesonet.NetParty.Tests
{
	[TestClass]
	public class LogTest
	{
		private ILog log;

		[TestInitialize()]
		public void ClassInit()
		{
			log = LogManager.GetLogger("DataDownloaderLogger");
		}

		[TestMethod]
		public void Log_Test()
		{
			log.Info("Info");
			log.Warn("Warning");
			log.Error("Error");
		}
	}
}
