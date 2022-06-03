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
    private readonly AccountService _acs;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;

    public ProfilesController(AccountService acs, KeepsService ks, VaultsService vs)
    {
      _acs = acs;
      _ks = ks;
      _vs = vs;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        Account profile = _acs.GetProfileByEmail(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}