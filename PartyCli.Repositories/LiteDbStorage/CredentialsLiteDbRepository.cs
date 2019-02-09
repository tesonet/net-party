using PartyCli.Entities;

namespace PartyCli.Repositories.LiteDbStorage
{
  public class CredentialsLiteDbRepository : LiteDbRepository<Credentials>
  {
    public override string CollectionName => "credentials";
  }
}
