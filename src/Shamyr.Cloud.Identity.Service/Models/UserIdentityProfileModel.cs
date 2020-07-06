using MongoDB.Bson;

namespace Shamyr.Cloud.Identity.Service.Models
{
  public class UserIdentityProfileModel
  {
    public ObjectId Id { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool Disabled { get; set; }
    public bool Admin { get; set; } = default!;
  }
}
