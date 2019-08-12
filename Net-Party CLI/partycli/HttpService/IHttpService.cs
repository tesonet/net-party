using partycli.Helpers;
using System.Threading.Tasks;

namespace partycli.Http
{
    public interface IHttpService
    {
        Task<IRequestResult<string>> GetWithToken(string token);
        Task<IRequestResult<string>> PostJson(string content);
    }
}
