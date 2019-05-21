using System.Threading.Tasks;
using NetParty.Contracts;

namespace NetParty.Services.Interfaces
{
    public interface IDisplayService
    {
        void DisplayTable(ServerDto[] servers);
        void DisplayText(string text);
    }
}
