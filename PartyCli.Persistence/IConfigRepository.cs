using System.Threading.Tasks;
using PartyCli.Contracts.Models;

namespace PartyCli.Persistence
{
	public interface IConfigRepository
	{
		Task Save(Config config);

		Task<Config> GetConfig();
	}
}