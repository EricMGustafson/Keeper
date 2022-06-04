using System.Data;
using checkpoint9.Models;
using Dapper;

namespace checkpoint9.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetProfileById(string id)
    {
      string sql = "SELECT * FROM profiles WHERE id = @id;";
      return _db.QueryFirstOrDefault<Profile>(sql, id);
    }
  }
}