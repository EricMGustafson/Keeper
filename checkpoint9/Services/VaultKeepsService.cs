using System;
using System.Collections.Generic;
using checkpoint9.Models;
using checkpoint9.Repositories;

namespace checkpoint9.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal List<VaultKeepViewModel> GetByVaultId(int id)
    {
      return _repo.GetByVaultId(id);
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
      return _repo.Create(vaultKeepData);
    }

    internal void Delete(int id, string userId)
    {
      VaultKeep found = GetById(id);
      if (found.CreatorId != userId)
      {
        throw new Exception("You may not delete this VaultKeep.");
      }
      _repo.Delete(id);
    }

  }
}