using System.Threading.Tasks;

namespace NetParty.Services.Interfaces
{
    public interface ISecurityService
    {
        Task<string> GetTokenAsync();
    }
}
