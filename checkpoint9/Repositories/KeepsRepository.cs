using System.Collections.Generic;
using System.Data;
using System.Linq;
using checkpoint9.Models;
using Dapper;

namespace checkpoint9.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> Get()
    {
      string sql = "SELECT a.*, k.* FROM keeps k JOIN accounts a ON k.creatorId = a.id;";
      return _db.Query<Account, Keep, Keep>(sql, (a, k) =>
      {
        k.Creator = a;
        return k;
      }).ToList();
    }

    internal Keep Get(int id)
    {
      string sql = "SELECT a.*, k.* FROM keeps k JOIN accounts a ON k.id = @id;";
      return _db.Query<Account, Keep, Keep>(sql, (a, k) =>
      {
        k.Creator = a;
        return k;
      }, new { id }).FirstOrDefault();
    }

    internal List<Keep> GetKeepsByProfileId(string id)
    {
      string sql = "SELECT a.*, k.* FROM keeps k JOIN accounts a ON k.creatorId = @id;";
      return _db.Query<Account, Keep, Keep>(sql, (a, k) =>
      {
        k.Creator = a;
        return k;
      }, new { id }).ToList();
    }

    internal Keep Create(Keep keepData)
    {
      string sql = "INSERT INTO keeps (name, description, img, creatorId) VALUES (@Name, @Description, @Img, @CreatorId); SELECT LAST_INSERT_ID();";
      keepData.Id = _db.ExecuteScalar<int>(sql, keepData);
      return keepData;
    }

    internal void Edit(Keep original)
    {
      string sql = "UPDATE keeps SET name = @Name, description = @Description, img = @Img WHERE id = @id;";
      _db.Execute(sql, original);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}