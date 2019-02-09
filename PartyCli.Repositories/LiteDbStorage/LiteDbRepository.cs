using LiteDB;
using PartyCli.Entities.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PartyCli.Repositories.LiteDbStorage
{
  public abstract class LiteDbRepository<T> : IRepository<T> where T: IEntity
  {
    private readonly string _dbname;    

    public LiteDbRepository()
    {
      var codeBase = Assembly.GetExecutingAssembly().CodeBase;

      _dbname = $"{Path.GetFileNameWithoutExtension(codeBase)}.db";
    }

    public abstract string CollectionName { get; }

    public ICollection<T> FindAll()
    {
      using (var db = new LiteDatabase(_dbname))
      {
        var dbEntities = db.GetCollection<T>(CollectionName);

        return dbEntities.FindAll().ToList();
      }
    }

    public void InsertBulk(IEnumerable<T> entities)
    {
      using (var db = new LiteDatabase(_dbname))
      {
        var dbEntities = db.GetCollection<T>(CollectionName);

        dbEntities.InsertBulk(entities);
      }
    }

    public void Truncate()
    {
      using (var db = new LiteDatabase(_dbname))
      {
        var dbEntities = db.GetCollection<T>(CollectionName);
        dbEntities.Delete(x => true);
      }
    }

  }
}
