using System.Threading.Tasks;

namespace NetParty.Data.Http
{
    public interface ITokenProvider
    {
        Task<string> GetToken();
    }
}
