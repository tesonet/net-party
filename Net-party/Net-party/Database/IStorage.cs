using System.Data.Linq;

namespace Net_party.Database
{
    public interface IStorage
    {
        DataContext GetContext();
    }
}
