using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tesonet.NetParty.Models.Interfaces;
using Tesonet.NetParty.Models.Models;
using Tesonet.NetParty.TextFileRepository;

namespace Tesonet.NetParty.Tests
{
	[TestClass]
	public class FileRepositoryTest
	{
		private IDataRepository _dataRepository;


		[TestInitialize()]
		public void ClassInit()
		{
			_dataRepository = new FileRepository();
		}

		[TestMethod]
		public void SaveToken_and_GetToken_Test()
		{
			var user = new User
			{
				Username = "Tessa",
				Password = "123456"
			};
			_dataRepository.SaveUser(user);
			var receivedUser = _dataRepository.GetUser();
			Assert.IsNotNull(receivedUser);
			Assert.AreEqual(user.Username, receivedUser.Username);
			Assert.AreEqual(user.Password, receivedUser.Password);
		}

		[TestMethod]
		public void SaveServerList_Test()
		{
			var list = new List<Server>
			{
				new Server
				{
					Name = "Test1",
					Distance = "1"
				},
				new Server
				{
					Name = "Test2",
					Distance = "2"
				}
			};
			_dataRepository.SaveServerList(list);
			var receivedList = _dataRepository.GetServerList();
			Assert.IsNotNull(receivedList);
			Assert.AreEqual(list.Count, receivedList.Count);
		}

		[TestMethod]
		public void SaveServerList_Null_Test()
		{
			_dataRepository.SaveServerList(null);
			var receivedList = _dataRepository.GetServerList();
			Assert.IsNull(receivedList);
		}
	}
}
