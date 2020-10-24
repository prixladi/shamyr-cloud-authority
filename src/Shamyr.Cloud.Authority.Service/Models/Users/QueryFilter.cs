using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.Users
{
  public record QueryFilter
  {
    [Range(0, int.MaxValue)]
    [Required]
    public int Skip { get; init; }

    [Range(1, 100)]
    [Required]
    public int Take { get; init; }

    public string? Username { get; init; }
    public string? Email { get; init; }
    public bool? Admin { get; init; }
  }
}
