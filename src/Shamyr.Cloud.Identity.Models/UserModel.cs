namespace Shamyr.Cloud.Identity.Service.Models
{
  public record UserModel
  {
    public string Id { get; init; } = default!;
    public string Username { get; init; } = default!;
    public string Email { get; init; } = default!;
    public bool Disabled { get; init; }
    public bool Admin { get; init; }
    public string? GivenName { get; init; }
    public string? FamilyName { get; init; }
    public bool Verified { get; init; }
  }
}
