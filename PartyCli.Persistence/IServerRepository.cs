using System.Collections.Generic;
using System.Threading.Tasks;
using PartyCli.Contracts.Models;

namespace PartyCli.Persistence
{
	public interface IServerRepository
	{
		Task Save(IEnumerable<Server> servers);

		Task<IEnumerable<Server>> GetServers();
	}
}