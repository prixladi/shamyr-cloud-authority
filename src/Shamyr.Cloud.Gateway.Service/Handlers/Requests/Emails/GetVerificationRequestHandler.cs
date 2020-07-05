using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Models;
using Shamyr.Cloud.Gateway.Service.Repositories.Users;
using Shamyr.Cloud.Gateway.Service.Requests.Emails;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.Emails
{
  public class GetVerificationRequestHandler: IRequestHandler<GetVerificationRequest, IdModel>
  {
    private readonly IUserRepository fUserRepository;

    public GetVerificationRequestHandler(IUserRepository userRepository)
    {
      fUserRepository = userRepository;
    }

    public async Task<IdModel> Handle(GetVerificationRequest request, CancellationToken cancellationToken)
    {
      var user = await GetUserByEmailOrThrowAsync(request.Email, cancellationToken);

      if (user.EmailToken is null)
        throw new ConflictException($"Account with email '{request.Email}' is already verified.");

      if (!await fUserRepository.TryUnsetEmailTokenAsync(user.Id, request.EmailToken, cancellationToken))
        throw new BadRequestException($"Invalid token.");

      return new IdModel(user.Id);
    }

    private async Task<UserDoc> GetUserByEmailOrThrowAsync(string email, CancellationToken cancellationToken)
    {
      var user = await fUserRepository.GetByEmailAsync(email, cancellationToken);
      if (user is null)
        throw new NotFoundException($"Account with email '{email}' does not exist.");

      return user;
    }
  }
}
