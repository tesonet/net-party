using System.Threading.Tasks;

namespace partycli
{
    public interface IRepositoryProvider
    {   
        void Reset();
        Task SaveAsync(string content);
        Task<string> LoadAsync();
    }
}
