using System.Collections.Generic;
using System.Data;
using System.Linq;
using checkpoint9.Models;
using Dapper;

namespace checkpoint9.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep GetMembership(int id, string userId)
    {
      string sql = "SELECT * FROM vaultkeeps vk WHERE keepId = @id;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal List<VaultKeepViewModel> GetByVaultId(int id)
    {
      string sql = @"
       SELECT
            a.*,
            k.*,
            vk.id AS vaultKeepId
        FROM vaultkeeps vk
        JOIN keeps k ON vk.keepId = k.id
        JOIN accounts a ON k.creatorId = a.id
        WHERE vk.vaultId = @id
      ;";
      return _db.Query<Account, VaultKeepViewModel, VaultKeepViewModel>(sql, (a, vk) =>
      {
        vk.Creator = a;
        return vk;
      }, new { id }).ToList();
    }

    internal VaultKeep GetById(int id)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE id = @id;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      string sql = "INSERT INTO vaultkeeps (keepId, vaultId, creatorId) VALUES (@KeepId, @VaultId, @CreatorId); SELECT LAST_INSERT_ID();";
      vaultKeepData.Id = _db.ExecuteScalar<int>(sql, vaultKeepData);
      return vaultKeepData;
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}