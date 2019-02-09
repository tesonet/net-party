using PartyCli.Entities;

namespace PartyCli.Repositories.JsonFileStorage
{
  public class ServerJsonFileRepository : JsonFileRepository<Server>
  {
    public override string JsonFileName => "servers.json";
  }
}
