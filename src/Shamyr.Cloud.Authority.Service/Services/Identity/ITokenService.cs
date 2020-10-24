using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Services.Identity
{
  public interface ITokenService
  {
    string GenerateUserJwt(UserDoc user, string grant);
    TokenDoc GenerateOrRenewRefreshToken(string? oldToken);
  }
}
