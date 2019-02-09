using PartyCli.Entities.Interfaces;

namespace PartyCli.Entities
{
    public class Credentials: IEntity
  {
      public int Id { get; set; }
      public string UserName { get; set; }      
      public string Password { get; set; }
  }
}
