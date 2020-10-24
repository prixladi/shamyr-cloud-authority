using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Models.Users;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Users;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Users
{
  public class GetRequestHandler: IRequestHandler<GetRequest, DetailModel>
  {
    private readonly IUserRepository fUserRepository;

    public GetRequestHandler(IUserRepository userRepository)
    {
      fUserRepository = userRepository;
    }

    public async Task<DetailModel> Handle(GetRequest request, CancellationToken cancellationToken)
    {
      var user = await fUserRepository.GetAsync(request.Id, cancellationToken);
      if (user is null)
        throw new NotFoundException($"User with ID '{request.Id}' does not exist.");

      return user.ToDetail();
    }
  }
}
