using System;
using checkpoint9.Interfaces;

namespace checkpoint9.Models
{
  public class VaultKeep : IRepoItem<int>
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int VaultId { get; set; }
    public int KeepId { get; set; }
  }
}