using System;
using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.ConnectToken
{
  public class TokensModel
  {
    [Required]
    public string BearerToken { get; set; } = default!;

    [Required]
    public string RefreshToken { get; set; } = default!;

    public DateTime? RefreshTokenExpirationUtc { get; set; }
  }
}
