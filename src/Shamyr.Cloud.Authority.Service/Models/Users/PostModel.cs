using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

using static Shamyr.Cloud.Authority.Service.Models.ModelConstants;

namespace Shamyr.Cloud.Authority.Service.Models.Users
{
  public record PostModel
  {
    [StringLength(_MaxUsernameLength, MinimumLength = _MinUsernameLength)]
    [Required]
    public string Username { get; init; } = default!;

    [StringLength(_MaxPasswordLength, MinimumLength = _MinPasswordLength)]
    [Required]
    public string Password { get; init; } = default!;

    [EmailAddress]
    [Required]
    public string Email { get; init; } = default!;

    [StringLength(_MaxGivenNameLength, MinimumLength = _MinGivenNameLength)]
    public string? GivenName { get; init; } = default!;

    [StringLength(_MaxFamilyNameLength, MinimumLength = _MinFamilyNameLength)]
    public string? FamilyName { get; init; } = default!;

    [Required]
    public ObjectId ClientId { get; init; }
  }
}
