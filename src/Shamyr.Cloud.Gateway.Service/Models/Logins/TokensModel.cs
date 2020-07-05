using System;
using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Gateway.Service.Models.Logins
{
  public class TokensModel
  {
    /// <summary>
    /// Access token
    /// </summary>
    [Required]
    public string BearerToken { get; set; } = default!;

    /// <summary>
    /// Refresh token
    /// </summary>
    [Required]
    public string RefreshToken { get; set; } = default!;

    /// <summary>
    /// Refresh token expiration time or <c>null</c> if expiration is unlimited
    /// </summary>
    public DateTime? RefreshTokenExpirationUtc { get; set; }
  }
}
