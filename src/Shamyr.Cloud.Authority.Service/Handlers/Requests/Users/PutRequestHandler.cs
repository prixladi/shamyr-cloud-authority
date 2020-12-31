using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Dtos.Users;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.CurrentUser;
using Shamyr.Cloud.Authority.Service.Services.Identity;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.CurrentUser
{
  public class PutRequestHandler: IRequestHandler<PutRequest>
  {
    private readonly IUserRepository fUserRepository;
    private readonly IIdentityService fIdentityService;

    public PutRequestHandler(IUserRepository userRepository, IIdentityService identityService)
    {
      fUserRepository = userRepository;
      fIdentityService = identityService;
    }

    public async Task<Unit> Handle(PutRequest request, CancellationToken cancellationToken)
    {
      if (await fUserRepository.ExistsByUsernameAsync(request.Model.Username, cancellationToken))
        throw new ConflictException($"User with username '{request.Model.Username}' already exists.");

      var updateDto = new UpdateDto(request.Model.Username, request.Model.GivenName, request.Model.FamilyName);
      await fUserRepository.UpdateAsync(fIdentityService.Current.UserId, updateDto, cancellationToken);

      return Unit.Value;
    }
  }
}
