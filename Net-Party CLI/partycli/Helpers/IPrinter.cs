using System.Collections.Generic;

namespace partycli.Helpers
{
    public interface IPrinter
    {
        void ServersList(List<Server> servers);
        void Info(string info);
        void Error(string error);
    }
}
