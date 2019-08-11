using System.Data.Linq;
using System.Threading.Tasks;
using Net_party.Entities;

namespace Net_party.Database
{
    public interface IStorage
    {
        DataContext GetContext();
    }
}
