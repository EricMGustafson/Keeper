using System;
using checkpoint9.Models;
using checkpoint9.Repositories;

namespace checkpoint9.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    private readonly VaultKeepsRepository _vkrepo;

    public VaultsService(VaultsRepository repo, VaultKeepsRepository vkrepo)
    {
      _repo = repo;
      _vkrepo = vkrepo;
    }

    internal Vault Get(int id, string userId)
    {
      Vault found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Vault ID not found.");
      }
      if (found.CreatorId != userId && userId != null)
      {
        throw new Exception("This is not your Vault.");
      }
      if (found.IsPrivate && found.CreatorId != userId)
      {
        VaultKeep membership = _vkrepo.GetMembership(id, userId);
        if (membership == null)
        {
          throw new Exception("You do not have access to this Vault.");
        }
      }
      return found;
    }

    internal Vault Create(Vault vaultData)
    {
      return _repo.Create(vaultData);
    }

    internal Vault Edit(Vault updateData)
    {
      Vault original = Get(updateData.Id, updateData.CreatorId);
      original.Name = updateData.Name ?? original.Name;
      original.Description = updateData.Description ?? original.Description;
      original.IsPrivate = updateData.IsPrivate == false ? updateData.IsPrivate : original.IsPrivate;
      _repo.Edit(original);
      return original;

    }

    internal void Delete(int id, string userId)
    {
      Vault vault = Get(id, userId);
      _repo.Delete(id);
    }
  }
}