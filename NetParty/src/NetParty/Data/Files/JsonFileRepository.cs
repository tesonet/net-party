using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetParty.Data.Files
{
    public class JsonFileRepository<TItem> : IRepository<TItem>
    {
        private readonly IRepository<byte[]> _rawRepo;

        public JsonFileRepository(IRepository<byte[]> rawRepo)
        {
            _rawRepo = rawRepo;
        }

        public async Task<TItem> GetAsync()
        {
            return JsonConvert.DeserializeObject<TItem>(
                Encoding.UTF8.GetString(await _rawRepo.GetAsync())
            );
        }

        public Task SaveAsync(TItem item)
        {
            return _rawRepo.SaveAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item)));
        }
    }
}
