using System;
using checkpoint9.Models;
using checkpoint9.Services;
using Microsoft.AspNetCore.Mvc;

namespace checkpoint9.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;

    public ProfilesController(ProfilesService ps, KeepsService ks, VaultsService vs)
    {
      _ps = ps;
      _ks = ks;
      _vs = vs;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        Profile profile = _ps.GetProfileById(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}