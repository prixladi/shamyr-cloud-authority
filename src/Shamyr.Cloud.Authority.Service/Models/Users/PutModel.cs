using System.ComponentModel.DataAnnotations;

using static Shamyr.Cloud.Authority.Service.Models.ModelConstants;

namespace Shamyr.Cloud.Authority.Service.Models.Users
{
  public record PutModel
  {
    [StringLength(_MaxUsernameLength, MinimumLength = _MinUsernameLength)]
    [Required]
    public string Username { get; init; } = default!;

    [StringLength(_MaxGivenNameLength, MinimumLength = _MinGivenNameLength)]
    public string? GivenName { get; init; } = default!;

    [StringLength(_MaxFamilyNameLength, MinimumLength = _MinFamilyNameLength)]
    public string? FamilyName { get; init; } = default!;
  }
}
