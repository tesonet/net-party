using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Tesonet.NetParty.Models.Interfaces;
using Tesonet.NetParty.Models.Models;

namespace Tesonet.NetParty.TextFileRepository
{
	public class FileRepository : IDataRepository
	{
		private readonly string _filePath = AppDomain.CurrentDomain.BaseDirectory + "\\FileRepository\\";
		private readonly string _token = "Token.json";
		private readonly string _serverList = "ServerList.json";

		public FileRepository()
		{
			if (!Directory.Exists(_filePath))
				Directory.CreateDirectory(_filePath);
		}

		public void SaveUser(User user)
		{
			var json = JsonConvert.SerializeObject(user);
			File.WriteAllText(_filePath + _token, json);
		}

		public void SaveServerList(IList<Server> serverList)
		{
			var json = JsonConvert.SerializeObject(serverList);
			File.WriteAllText(_filePath + _serverList, json);
		}

		public User GetUser()
		{
			var filePath = _filePath + _token;
			if (!File.Exists(filePath))
				return null;
			var json = File.ReadAllText(filePath);
			var user = JsonConvert.DeserializeObject<User>(json);

			return user;
		}

		public IList<Server> GetServerList()
		{
			var filePath = _filePath + _serverList;
			if (!File.Exists(filePath))
				return null;
			var json = File.ReadAllText(filePath);
			var list = JsonConvert.DeserializeObject<List<Server>>(json);

			return list;
		}
	}
}
