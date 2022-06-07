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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;

    public VaultsController(VaultsService vs, VaultKeepsService vks)
    {
      _vs = vs;
      _vks = vks;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> Get(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        Vault vault = _vs.Get(id, userInfo?.Id);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<VaultKeepViewModel>> GetKeepsByVault(int id)
    {
      try
      {
        List<VaultKeepViewModel> vaultKeeps = _vs.GetByVaultId(id);
        return Ok(vaultKeeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
      try
      {
        Profile profile = await HttpContext.GetUserInfoAsync<Profile>();
        vaultData.CreatorId = profile.Id;
        Vault newVault = _vs.Create(vaultData);
        newVault.UpdatedAt = DateTime.UtcNow;
        newVault.CreatedAt = DateTime.UtcNow;
        newVault.Creator = profile;
        return Ok(newVault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault updateData)
    {
      try
      {
        Profile profile = await HttpContext.GetUserInfoAsync<Profile>();
        updateData.CreatorId = profile.Id;
        updateData.Id = id;
        Vault updatedVault = _vs.Edit(updateData);
        return Ok(updatedVault);
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
        _vs.Delete(id, profile.Id);
        return Ok("Deleted...");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}