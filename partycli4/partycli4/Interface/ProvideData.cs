
using System.Collections.Generic;
using partycli4.Data;

namespace partycli4.Interface
{
    public interface IProvideData
    {
 
        string GetGeneratedToken(User user);
        IList<Server> GetServerList(string accessToken);
    }
}
