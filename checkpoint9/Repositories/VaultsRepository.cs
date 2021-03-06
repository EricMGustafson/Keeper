using System.Collections.Generic;
using System.Data;
using System.Linq;
using checkpoint9.Models;
using Dapper;

namespace checkpoint9.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault Get(int id)
    {
      string sql = "SELECT a.*, v.* FROM vaults v JOIN accounts a ON v.creatorId = a.id WHERE v.id = @id;";
      return _db.Query<Account, Vault, Vault>(sql, (a, v) =>
      {
        v.Creator = a;
        return v;
      }, new { id }).FirstOrDefault();
    }

    internal List<Vault> GetVaultsByProfileId(string id)
    {
      string sql = "SELECT a.*, v.* FROM vaults v JOIN accounts a ON v.creatorId = a.id WHERE v.creatorId = @id;";
      return _db.Query<Account, Vault, Vault>(sql, (a, v) =>
      {
        v.Creator = a;
        return v;
      }, new { id }).ToList();
    }

    internal Vault Create(Vault vaultData)
    {
      string sql = "INSERT INTO vaults (name, description, image, isPrivate, creatorId) VALUES (@Name, @Description, @Image, @IsPrivate, @CreatorId); SELECT LAST_INSERT_ID();";
      vaultData.Id = _db.ExecuteScalar<int>(sql, vaultData);
      return vaultData;
    }

    internal void Edit(Vault original)
    {
      string sql = "UPDATE vaults SET name = @Name, description = @Description, image = @Image, isPrivate = @IsPrivate, creatorId = @CreatorId WHERE id = @Id;";
      _db.Execute(sql, original);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

  }
}