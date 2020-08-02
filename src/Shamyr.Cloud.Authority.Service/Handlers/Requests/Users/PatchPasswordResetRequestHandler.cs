using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Users;
using Shamyr.Cloud.Services;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Users
{
  public class PatchPasswordResetRequestHandler: IRequestHandler<PatchPasswordResetRequest>
  {
    private readonly ISecretService fSecretService;
    private readonly IUserRepository fUserRepository;

    public PatchPasswordResetRequestHandler(ISecretService secretService, IUserRepository userRepository)
    {
      fSecretService = secretService;
      fUserRepository = userRepository;
    }

    public async Task<Unit> Handle(PatchPasswordResetRequest request, CancellationToken cancellationToken)
    {
      var user = await GetUserByIdOrThrowAsync(request.UserId, cancellationToken);

      if (user.PasswordToken is null)
        throw new ConflictException($"User with name '{user.Username}' and email '{user.Email}' doesn't have password reset requested.");

      if (user.PasswordToken != request.Model.PasswordToken)
        throw new BadRequestException("Password reset token is invalid.");

      var secret = fSecretService.CreateSecret(request.Model.Password);
      await fUserRepository.SetUserSecretAsync(user.Id, secret.ToDoc(), cancellationToken);

      return Unit.Value;
    }

    private async Task<UserDoc> GetUserByIdOrThrowAsync(ObjectId userId, CancellationToken cancellationToken)
    {
      var user = await fUserRepository.GetAsync(userId, cancellationToken);
      if (user is null)
        throw new NotFoundException($"User with ID '{userId}' does not exist.");

      return user;
    }
  }
}
