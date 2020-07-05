using System;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Gateway.Service.Services.Identity
{
  public interface ITokenService
  {
    TokenDoc GenerateOrRenewRefreshToken(string? oldToken);
    string GenerateUserJwt(ObjectId userId, DateTime? issuedAtUtc = null);
    (ObjectId, DateTime) ValidateJwtWithoutExpiration(string jwtString);
  }
}
