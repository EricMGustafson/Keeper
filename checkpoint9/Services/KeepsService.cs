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

    internal List<Keep> GetKeepsByProfileId(string id)
    {
      if (id == null)
      {
        throw new Exception("Invalid Profile Id");
      }
      return _repo.GetKeepsByProfileId(id);
    }

    internal Keep Get(int id, string userId)
    {
      Keep found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Keep ID not found.");
      }
      if (found.CreatorId != userId)
      {
        ++found.Views;
        _repo.Edit(found);
      }
      return found;
    }

    internal Keep Create(Keep keepData)
    {
      return _repo.Create(keepData);
    }

    internal void HasAccess(string originalId, string accessorId)
    {
      if (originalId != accessorId)
      {
        throw new Exception("This is not your keep");
      }
    }

    internal Keep Edit(Keep updateData)
    {
      Keep original = Get(updateData.Id, updateData.CreatorId);
      HasAccess(original.CreatorId, updateData.CreatorId);
      original.Name = updateData.Name ?? original.Name;
      original.Description = updateData.Description ?? original.Description;
      original.Img = updateData.Img ?? original.Img;
      _repo.Edit(original);
      return original;

    }


    internal void Delete(int id, string userId)
    {
      Keep keep = Get(id, userId);
      HasAccess(keep.CreatorId, userId);
      _repo.Delete(id);
    }
  }

}