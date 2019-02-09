using PartyCli.Entities;

namespace PartyCli.Repositories.JsonFileStorage
{
  public class CredentialsJsonFileRepository : JsonFileRepository<Credentials>
  {
    public override string JsonFileName => "credentials.json";
  }
}
