using System.Collections.Generic;
using System.Threading.Tasks;
using PartyCli.Contracts.Models;

namespace PartyCli.Core.Services
{
	public interface IPlaygroundService
	{
		Task<IEnumerable<Server>> GetServers(Config config);
	}
}