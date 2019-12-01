using System.Collections.Generic;
using Tesonet.NetParty.Models.Models;

namespace Tesonet.NetParty.Models.Interfaces
{
	public interface IDataProvider
	{
		string GetAuthenticationToken(User user);
		IList<Server> GetServiceList(string accessToken);
	}
}
