using System.Threading.Tasks;

namespace NetParty.Data
{
    public interface IReadOnlyRepository<TItem>
    {
        Task<TItem> GetAsync();
    }
}
