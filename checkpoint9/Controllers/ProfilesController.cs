using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using checkpoint9.Models;
using checkpoint9.Services;
using CodeWorks.Auth0Provider;
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

    public async Task<ActionResult<Profile>> GetUserInfo()
    {
      Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
      return userInfo;
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

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfileId(string id)
    {
      try
      {
        List<Keep> profileKeeps = _ks.GetKeepsByProfileId(id);
        return Ok(profileKeeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaultsByProfileIdAsync(string id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        List<Vault> profileVaults = _vs.GetVaultsByProfileId(id);
        return Ok(profileVaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}