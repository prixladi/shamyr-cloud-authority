using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Identity.Service.Extensions;
using Shamyr.Cloud.Identity.Service.Models;
using Shamyr.Cloud.Identity.Service.Repositories;
using Shamyr.Cloud.Identity.Service.Requests.Users;

namespace Shamyr.Cloud.Identity.Service.Handlers.Requests.Users
{
  public class GetRequestHandler: IRequestHandler<GetRequest, UserModel>
  {
    private readonly IUserRepository fUserRepository;

    public GetRequestHandler(IUserRepository userRepository)
    {
      fUserRepository = userRepository;
    }

    public async Task<UserModel> Handle(GetRequest request, CancellationToken cancellationToken)
    {
      var user = await fUserRepository.GetAsync(request.UserId, cancellationToken);
      if (user is null)
        throw new NotFoundException(nameof(user));

      return user.ToDetail();
    }
  }
}
