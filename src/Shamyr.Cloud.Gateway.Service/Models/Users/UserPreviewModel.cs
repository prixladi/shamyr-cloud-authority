using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Shamyr.Cloud.Gateway.Service.Models.Users
{
  public class UserPreviewModel
  {
    [Required]
    public ObjectId Id { get; set; }

    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public bool Disabled { get; set; }

    [Required]
    public bool Admin { get; set; } = default!;
  }
}
