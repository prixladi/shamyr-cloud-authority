using MongoDB.Bson;
using Shamyr.Cloud.Database;

namespace Shamyr.Cloud.Identity.Service.Dtos
{
  public class UserIdentityProfileDto
  {
    public ObjectId Id { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool Disabled { get; set; }
    public bool Admin { get; set; }
    public bool Verified { get; set; }
  }
}
