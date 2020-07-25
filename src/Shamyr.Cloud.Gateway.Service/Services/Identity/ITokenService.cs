using System;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Dtos;

namespace Shamyr.Cloud.Gateway.Service.Services.Identity
{
  public interface ITokenService
  {
    TokenDoc GenerateOrRenewRefreshToken(string? oldToken);
    string GenerateUserJwt(UserDoc user, DateTime? issuedAtUtc = null);
    ValidatedJwtDto? ValidateJwtWithoutExpiration(string jwtString);
  }
}
