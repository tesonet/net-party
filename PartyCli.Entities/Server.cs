using PartyCli.Entities.Interfaces;

namespace PartyCli.Entities
{
  public class Server: IEntity
  {
    public int Id { get; set; } // required by LiteDb only
    public string Name { get; set; }
    public int Distance { get; set; }
  }
}
