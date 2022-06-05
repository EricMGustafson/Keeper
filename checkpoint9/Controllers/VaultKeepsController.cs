using System;
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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;

    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        vaultKeepData.CreatorId = userInfo.Id;
        VaultKeep vaultKeep = _vks.Create(vaultKeepData);
        vaultKeep.CreatedAt = DateTime.UtcNow;
        vaultKeep.UpdatedAt = DateTime.UtcNow;
        vaultKeep.Creator = userInfo;
        return Ok(vaultKeep);
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
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        _vks.Delete(id, userInfo.Id);
        return Ok("Deleted...");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}