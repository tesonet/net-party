using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tesonet.NetParty.Models.Interfaces;
using Tesonet.NetParty.Models.Models;
using Tesonet.NetParty.Services;

namespace Tesonet.NetParty.Tests
{
	[TestClass]
	public class DataProviderTest
	{
		private IDataProvider _dataProvider;
		private readonly User user = new User
		{
			Username = "tesonet" ,
			Password = "partyanimal"
		};

		[TestInitialize()]
		public void ClassInit()
		{
			_dataProvider = new PlayGroundService();
		}

		[TestMethod]
		public void GetAuthenticationToken_Test()
		{
			var token = _dataProvider.GetAuthenticationToken(user);
			Assert.IsFalse(string.IsNullOrEmpty(token));
		}

		[TestMethod]
		public void GetAuthenticationToken_And_GetServiceList_Test()
		{
			var token = _dataProvider.GetAuthenticationToken(user);
			var list = _dataProvider.GetServiceList(token);
			Assert.IsTrue(list.Count > 0);
		}
	}
}
