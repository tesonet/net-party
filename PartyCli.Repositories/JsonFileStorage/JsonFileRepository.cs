using Newtonsoft.Json;
using PartyCli.Entities.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PartyCli.Repositories.JsonFileStorage
{
  public abstract class JsonFileRepository<T> : IRepository<T> where T : IEntity
  {
    public abstract string JsonFileName { get; }

    public ICollection<T> FindAll()
    {
      List<T> entities = new List<T>();

      if (File.Exists(JsonFileName))
      {
        using (StreamReader reader = new StreamReader(JsonFileName))
        {
          string json = reader.ReadToEnd();
          entities = JsonConvert.DeserializeObject<List<T>>(json);
        }
      }

      return entities;
    }

    public void InsertBulk(IEnumerable<T> entities)
    {
      var joinEntities = FindAll()
        .Concat(entities);

      using (StreamWriter file = File.CreateText(JsonFileName))
      {
        JsonSerializer serializer = new JsonSerializer();
        serializer.Formatting = Formatting.Indented;
        serializer.Serialize(file, joinEntities);
      }
    }

    public void Truncate()
    {
      IEnumerable<T> entities = new List<T>();

      using (StreamWriter file = File.CreateText(JsonFileName))
      {
        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(file, entities);
      }
    }
  }
}
