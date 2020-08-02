using System;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Authority.Service.Dtos;

namespace Shamyr.Cloud.Authority.Service.Services.Identity
{
  public interface ITokenService
  {
    TokenDoc GenerateOrRenewRefreshToken(string? oldToken);
    string GenerateUserJwt(UserDoc user, DateTime? issuedAtUtc = null);
    ValidatedJwtDto? ValidateJwtWithoutExpiration(string jwtString);
  }
}
