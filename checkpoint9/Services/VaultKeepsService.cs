using System;
using System.Collections.Generic;
using checkpoint9.Models;
using checkpoint9.Repositories;

namespace checkpoint9.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsRepository _vrepo;
    private readonly KeepsRepository _krepo;

    public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vrepo, KeepsRepository krepo)
    {
      _repo = repo;
      _vrepo = vrepo;
      _krepo = krepo;
    }

    internal List<VaultKeepViewModel> GetByVaultId(int id)
    {
      Vault vault = _vrepo.Get(id);
      if (!vault.IsPrivate)
      {
        return _repo.GetByVaultId(id);
      }
      else
      {
        throw new Exception("This Vault is Private.");
      }

    }

    private VaultKeep GetById(int id)
    {
      VaultKeep found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("VaultKeep not found.");
      }
      return found;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      Vault vault = _vrepo.Get(vaultKeepData.VaultId);
      if (vault.CreatorId != vaultKeepData.CreatorId)
      {
        throw new Exception("You can not add keeps to Vaults that you do not own.");
      }
      Keep keep = _krepo.Get(vaultKeepData.KeepId);
      keep.Kept++;
      _krepo.Edit(keep);
      return _repo.Create(vaultKeepData);
    }

    internal void Delete(int id, string userId)
    {
      VaultKeep found = GetById(id);
      if (found.CreatorId != userId)
      {
        throw new Exception("You may not delete this VaultKeep.");
      }
      Keep keep = _krepo.Get(found.KeepId);
      keep.Kept--;
      _krepo.Edit(keep);
      _repo.Delete(id);
    }

  }
}