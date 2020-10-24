using System;
using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.Token
{
  public record TokensModel
  {
    [Required]
    public string BearerToken { get; init; } = default!;

    [Required]
    public string RefreshToken { get; init; } = default!;

    public DateTime? RefreshTokenExpirationUtc { get; init; }
  }
}
