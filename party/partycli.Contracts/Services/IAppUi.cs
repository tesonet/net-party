using System.Collections.Generic;
using partycli.Contracts.DTOs;

namespace partycli.Contracts.Services
{
    public interface IAppUi
    {
        void Show(IEnumerable<ServerDTO> servers);
        void Show(string message);
    }
}
