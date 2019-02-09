namespace PartyCli.Repositories.Interfaces
{
  public enum StorageType
  {    
    LiteDb = 0,
    JsonFile = 1,
  }

  public interface IRepositorySettings
  {
    StorageType StorageType { get; set; }
  }
}
