using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using Shamyr.Cloud.Identity.Service.Extensions;
using Shamyr.Cloud.Identity.Service.Protos;
using Shamyr.Cloud.Identity.Service.Repositories;

namespace Shamyr.Cloud.Identity.Service.Services.Grpc
{
  [Authorize]
  public class UsersService: Users.UsersBase
  {
    private readonly ITokenService fTokenService;
    private readonly IUserRepository fUserRepository;

    public UsersService(ITokenService tokenService, IUserRepository userRepository)
    {
      fTokenService = tokenService;
      fUserRepository = userRepository;
    }

    public override async Task<GetByIdReply> GetById(GetByIdRequest request, ServerCallContext context)
    {
      if (!ObjectId.TryParse(request.UserId, out var userId))
        throw new BadRequestException("User id has invalid format");

      var user = await fUserRepository.GetAsync(userId, context.CancellationToken);
      if (user is null)
        throw new NotFoundException(nameof(user));

      return new GetByIdReply
      {
        ProfileMessage = user.ToMessage()
      };
    }

    public override async Task<GetByTokenReply> GetByToken(GetByTokenRequest request, ServerCallContext context)
    {
      var (result, dto) = await fTokenService.ValidateTokenAsync(request.Token, context.CancellationToken);

      return new GetByTokenReply
      {
        IdentityResult = (GetByTokenReply.Types.IdentityResult)result,
        ProfileMessage = dto?.ToMessage()
      };
    }
  }
}
