using MongoDB.Bson;

namespace Shamyr.Cloud.Identity.Service.Dtos
{
  public record UserIdentityProfileDto
  {
    public ObjectId Id { get; init; }
    public string? Username { get; init; } = default!;
    public string Email { get; init; } = default!;
    public bool Disabled { get; init; }
    public bool Admin { get; init; }
    public bool Verified { get; init; }
  }
}
