using System.Collections.Generic;
using System.Data;
using System.Linq;
using checkpoint9.Models;
using Dapper;

namespace checkpoint9.Repositories
{
  public class AccountsRepository
  {
    private readonly IDbConnection _db;

    public AccountsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Account GetByEmail(string userEmail)
    {
      string sql = "SELECT * FROM accounts WHERE email = @userEmail";
      return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
    }

    internal Account GetById(string id)
    {
      string sql = "SELECT * FROM accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Account>(sql, new { id });
    }

    internal List<Vault> GetMyVaults(string id)
    {
      string sql = "SELECT a.*, v.* FROM vaults v JOIN accounts a ON v.creatorId = a.id WHERE v.creatorId = @id;";
      return _db.Query<Account, Vault, Vault>(sql, (a, v) =>
      {
        v.Creator = a;
        return v;
      }, new { id }).ToList();
    }

    internal Account Create(Account newAccount)
    {
      string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
      _db.Execute(sql, newAccount);
      return newAccount;
    }

    internal Account Edit(Account update)
    {
      string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
      _db.Execute(sql, update);
      return update;
    }

  }
}
