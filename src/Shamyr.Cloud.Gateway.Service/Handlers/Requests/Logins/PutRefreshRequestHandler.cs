using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Models.Logins;
using Shamyr.Cloud.Gateway.Service.Repositories;
using Shamyr.Cloud.Gateway.Service.Requests.Logins;
using Shamyr.Cloud.Gateway.Service.Services.Identity;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.Logins
{
  public class PutRefreshRequestHandler: IRequestHandler<PutRefreshRequest, TokensModel>
  {
    private readonly IUserRepository fUserRepository;
    private readonly ITokenService fTokenService;
    private readonly IUserTokenRepository fUserTokenRepository;

    public PutRefreshRequestHandler(
      IUserRepository userRepository,
      ITokenService tokenService,
      IUserTokenRepository userTokenRepository)
    {
      fUserRepository = userRepository;
      fTokenService = tokenService;
      fUserTokenRepository = userTokenRepository;
    }

    public async Task<TokensModel> Handle(PutRefreshRequest request, CancellationToken cancellationToken)
    {
      var (userId, issuedAtUtc) = fTokenService.ValidateJwtWithoutExpiration(request.Model.BearerToken);
      var user = await ValidateResultAsync(userId, issuedAtUtc, request.Model.RefreshToken, cancellationToken);

      string jwt = fTokenService.GenerateUserJwt(userId, issuedAtUtc);

      var userToken = fTokenService.GenerateOrRenewRefreshToken(user.RefreshToken?.Value);
      await fUserTokenRepository.SetRefreshTokenAsync(userId, userToken, cancellationToken);

      return new TokensModel
      {
        BearerToken = jwt,
        RefreshToken = userToken.Value,
        RefreshTokenExpirationUtc = userToken.ExpirationUtc,
      };
    }

    private async Task<UserDoc> ValidateResultAsync(ObjectId userId, DateTime issuedAtUtc, string refreshToken, CancellationToken cancellationToken)
    {
      if (userId == ObjectId.Empty)
        throw new BadRequestException("Bearer token is not valid.");

      var user = await fUserRepository.GetAsync(userId, cancellationToken);

      if (user is null)
        throw new ForbiddenException($"User with ID '{userId}' was not found.");

      var userToken = user.RefreshToken;

      if (user.LogoutUtc.HasValue && user.LogoutUtc.Value > issuedAtUtc)
        throw new BadRequestException("User is logged out");

      if (userToken is null || userToken.Value != refreshToken)
        throw new BadRequestException("Refresh token is not valid.");

      if (userToken.ExpirationUtc < DateTime.UtcNow)
        throw new BadRequestException("Refresh token expired.");

      return user;
    }
  }
}
