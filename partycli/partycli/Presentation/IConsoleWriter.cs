using partycli.core.Repositories.Model;
using System.Collections.Generic;

namespace partycli.Presentation
{
    public interface IConsoleWriter
    {
        void Write(string message);
        void Write(IEnumerable<Server> servers);
    }
}