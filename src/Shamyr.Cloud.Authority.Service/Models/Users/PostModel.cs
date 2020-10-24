using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Shamyr.Cloud.Authority.Service.Models.Users
{
  public record PostModel
  {
    [StringLength(maximumLength: ModelConstants._MaxUsernameLength, MinimumLength = ModelConstants._MinUsernameLength)]
    [Required]
    public string Username { get; init; } = default!;

    [StringLength(maximumLength: ModelConstants._MaxPasswordLength, MinimumLength = ModelConstants._MinPasswordLength)]
    [Required]
    public string Password { get; init; } = default!;

    [EmailAddress]
    [Required]
    public string Email { get; init; } = default!;

    public string? GivenName { get; init; } = default!;

    public string? FamilyName { get; init; } = default!;

    [Required]
    public ObjectId ClientId { get; init; }
  }
}
