using System.Collections.Generic;
using System.Threading.Tasks;
using PartyCli.Contracts.Models;

namespace PartyCli.Core.Api
{
	public interface IPlaygroundApiClient
	{
		Task<string> GetToken(string userName, string password);
		Task<IEnumerable<Server>> GetServers(string token);
	}
}