using System;
using checkpoint9.Models;
using checkpoint9.Repositories;

namespace checkpoint9.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _repo;

    public ProfilesService(ProfilesRepository repo)
    {
      _repo = repo;
    }

    internal Profile GetProfileById(string id)
    {
      Profile found = _repo.GetProfileById(id);
      if (found == null)
      {
        throw new Exception("Invalid Profile Id.");
      }
      return found;
    }
  }
}