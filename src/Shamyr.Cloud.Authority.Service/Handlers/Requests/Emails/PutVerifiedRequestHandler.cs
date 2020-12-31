using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Models;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Emails;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Emails
{
  public class PutVerifiedRequestHandler: IRequestHandler<PutVerifiedRequest, IdModel>
  {
    private readonly IUserRepository fUserRepository;

    public PutVerifiedRequestHandler(IUserRepository userRepository)
    {
      fUserRepository = userRepository;
    }

    public async Task<IdModel> Handle(PutVerifiedRequest request, CancellationToken cancellationToken)
    {
      var user = await GetUserByEmailOrThrowAsync(request.Email, cancellationToken);
      if (user.EmailToken is null)
        throw new ConflictException($"Account with email '{request.Email}' is already verified.");

      if (!await fUserRepository.TryUnsetEmailTokenAndSetVerifiedAsync(user.Id, request.EmailToken, cancellationToken))
        throw new BadRequestException($"Invalid token.");

      return new IdModel { Id = user.Id };
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
