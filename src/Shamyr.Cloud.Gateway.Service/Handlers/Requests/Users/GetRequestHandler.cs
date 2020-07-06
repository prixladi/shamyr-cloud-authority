using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Extensions;
using Shamyr.Cloud.Gateway.Service.Models.Users;
using Shamyr.Cloud.Gateway.Service.Repositories.Users;
using Shamyr.Cloud.Gateway.Service.Requests.Users;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.Users
{
  public class GetRequestHandler: IRequestHandler<GetRequest, UserDetailModel>
  {
    private readonly IUserRepository fUserRepository;

    public GetRequestHandler(IUserRepository userRepository)
    {
      fUserRepository = userRepository;
    }

    public async Task<UserDetailModel> Handle(GetRequest request, CancellationToken cancellationToken)
    {
      var user = await fUserRepository.GetAsync(request.Id, cancellationToken);
      if (user is null)
        throw new NotFoundException($"User with ID '{request.Id}' does not exist.");

      return user.ToDetailModel();
    }
  }
}
