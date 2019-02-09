using PartyCli.Entities;

namespace PartyCli.Repositories.LiteDbStorage
{
    public class ServerLiteDbRepository : LiteDbRepository<Server>
    {
      public override string CollectionName => "servers";
    }
}
