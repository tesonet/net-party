using System;
using System.Threading.Tasks;

namespace partycli.Http
{
    public interface IHttpService
    {
        Task<string> GetWithToken(string token);
        Task<dynamic> PostJson(string content);
    }
}
