using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Models.Logins;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Logins;
using Shamyr.Cloud.Authority.Service.Services.Identity;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Logins
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
      var dto = fTokenService.ValidateJwtWithoutExpiration(request.Model.BearerToken);
      if (dto == null)
        throw new BadRequestException("Bearer token is not valid.");

      var user = await fUserRepository.GetAsync(dto.UserId, cancellationToken);
      if (user is null)
        throw new ForbiddenException($"User with ID '{dto.UserId}' was not found.");
      if (user.Disabled)
        throw new UserDisabledException();
      if (user.LogoutUtc.HasValue && user.LogoutUtc.Value > dto.IssuedAtUtc)
        throw new BadRequestException("User is logged out");
      if (user.RefreshToken is null || user.RefreshToken.Value != request.Model.RefreshToken)
        throw new BadRequestException("Refresh token is not valid.");
      if (user.RefreshToken.ExpirationUtc < DateTime.UtcNow)
        throw new BadRequestException("Refresh token expired.");

      string jwt = fTokenService.GenerateUserJwt(user, dto.IssuedAtUtc);
      var newToken = fTokenService.GenerateOrRenewRefreshToken(user.RefreshToken?.Value);
      await fUserTokenRepository.SetRefreshTokenAsync(user.Id, newToken, cancellationToken);

      return newToken.ToTokensModel(jwt);
    }
  }
}
