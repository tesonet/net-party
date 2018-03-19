using System.Collections.Generic;

namespace Tesonet
{
    public interface ITesonetService
    {
        Token GetAccessToken(string username, string password);

        IList<Server> GetServerList(Token token);
    }
}
