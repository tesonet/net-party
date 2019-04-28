using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetParty.Application.Interfaces
{
    public interface ICommand
    {
        bool TryParseParameters(ICommandArgs args);
        void SetParameters(IDictionary<string, string> commandParameters);
    }
}