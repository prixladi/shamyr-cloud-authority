namespace Shamyr.Cloud.Authority.Models
{
  public record UserModel
  {
    public string Id { get; init; } = default!;
    public string Username { get; init; } = default!;
    public string Email { get; init; } = default!;
    public bool Admin { get; init; }
    public string GivenName { get; init; } = default!;
    public string FamilyName { get; init; } = default!;
  }
}
