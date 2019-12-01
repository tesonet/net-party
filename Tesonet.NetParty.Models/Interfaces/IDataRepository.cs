using System.Collections.Generic;
using Tesonet.NetParty.Models.Models;

namespace Tesonet.NetParty.Models.Interfaces
{
	public interface IDataRepository
	{
		void SaveUser(User user);
		void SaveServerList(IList<Server> serverList);
		User GetUser();
		IList<Server> GetServerList();

	}
}
