using System;
using System.Collections.Generic;
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
      if (found.IsPrivate && found.CreatorId != userId)
      {
        throw new Exception("You do not have access to this Vault.");
      }
      return found;
    }

    internal List<VaultKeepViewModel> GetByVaultId(int id, string userId)
    {
      Vault vault = _repo.Get(id);
      if (vault.IsPrivate && vault.CreatorId != userId)
      {
        throw new Exception("This Vault is Private.");
      }
      else
      {
        return _vkrepo.GetByVaultId(id);
      }
    }

    internal List<Vault> GetVaultsByProfileId(string id)
    {
      if (id == null)
      {
        throw new Exception("Invalid Profile Id");
      }
      List<Vault> allVaults = _repo.GetVaultsByProfileId(id);
      List<Vault> publicVaults = allVaults.FindAll(v => !v.IsPrivate);
      return publicVaults;
    }

    internal Vault Create(Vault vaultData)
    {
      return _repo.Create(vaultData);
    }

    internal Vault Edit(Vault updateData)
    {
      Vault original = Get(updateData.Id, updateData.CreatorId);
      if (updateData.CreatorId != original.CreatorId)
      {
        throw new Exception("This vault does not belong to you.");
      }
      original.Name = updateData.Name ?? original.Name;
      original.Image = updateData.Image ?? original.Image;
      original.Description = updateData.Description ?? original.Description;
      original.IsPrivate = updateData.IsPrivate == false ? updateData.IsPrivate : original.IsPrivate;
      _repo.Edit(original);
      return original;

    }

    internal void Delete(int id, string userId)
    {
      Vault vault = Get(id, userId);
      if (vault.CreatorId != userId)
      {
        throw new Exception("You do not have access to this Vault.");
      }
      _repo.Delete(id);
    }
  }
}