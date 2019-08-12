using System.Threading.Tasks;

namespace partycli.Repository
{
    public interface IRepositoryProvider
    {   
        void Reset();
        Task SaveAsync(string content);
        Task<string> LoadAsync();
    }
}
