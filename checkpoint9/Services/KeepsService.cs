using System;
using System.Collections.Generic;
using checkpoint9.Models;
using checkpoint9.Repositories;

namespace checkpoint9.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal List<Keep> Get()
    {
      return _repo.Get();
    }

    internal Keep Get(int id, string userId)
    {
      Keep found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Keep ID not found.");
      }
      if (found.CreatorId != userId && userId != null)
      {
        throw new Exception("This is not your Keep");
      }
      return found;
    }

    internal Keep Create(Keep keepData)
    {
      return _repo.Create(keepData);
    }

    internal Keep Edit(Keep updateData)
    {
      Keep original = Get(updateData.Id, updateData.CreatorId);
      original.Name = updateData.Name ?? original.Name;
      original.Description = updateData.Description ?? original.Description;
      original.Img = updateData.Img ?? original.Img;
      _repo.Edit(original);
      return original;

    }

    internal void Delete(int id, string userId)
    {
      Keep keep = Get(id, userId);
      _repo.Delete(id);
    }
  }

}