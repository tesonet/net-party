using System.Threading.Tasks;

namespace NetParty.Data
{
    public interface IRepository<TItem> : IReadOnlyRepository<TItem>
    {
        Task SaveAsync(TItem item);
    }
}
