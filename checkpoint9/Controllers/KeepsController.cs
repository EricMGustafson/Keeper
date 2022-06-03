using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using checkpoint9.Models;
using checkpoint9.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace checkpoint9.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;

    public KeepsController(KeepsService ks)
    {
      _ks = ks;

    }

    [HttpGet]
    public ActionResult<List<Keep>> Get()
    {
      try
      {
        List<Keep> keeps = _ks.Get();
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Keep>> Get(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        Keep keep = _ks.Get(id, userInfo?.Id);
        return Ok(keep);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
    {
      try
      {
        Profile profile = await HttpContext.GetUserInfoAsync<Profile>();
        keepData.CreatorId = profile.Id;
        Keep newKeep = _ks.Create(keepData);
        newKeep.Creator = profile;
        newKeep.CreatedAt = DateTime.UtcNow;
        newKeep.UpdatedAt = DateTime.UtcNow;
        return Ok(newKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Edit(int id, [FromBody] Keep updateData)
    {
      try
      {
        Profile profile = await HttpContext.GetUserInfoAsync<Profile>();
        updateData.CreatorId = profile.Id;
        updateData.Id = id;
        Keep updatedKeep = _ks.Edit(updateData);
        return Ok(updatedKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Profile profile = await HttpContext.GetUserInfoAsync<Profile>();
        _ks.Delete(id, profile.Id);
        return Ok("Deleted...");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}