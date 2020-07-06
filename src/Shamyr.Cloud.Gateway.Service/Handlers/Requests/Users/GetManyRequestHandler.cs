using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Extensions;
using Shamyr.Cloud.Gateway.Service.Models.Users;
using Shamyr.Cloud.Gateway.Service.OrderDefinitions;
using Shamyr.Cloud.Gateway.Service.Repositories.Users;
using Shamyr.Cloud.Gateway.Service.Requests.Users;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.Users
{
  public class GetManyRequestHandler: IRequestHandler<GetManyRequest, UserPreviewsModel>
  {
    private readonly IUserRepository fUserRepository;

    public GetManyRequestHandler(IUserRepository userRepository)
    {
      fUserRepository = userRepository;
    }

    public async Task<UserPreviewsModel> Handle(GetManyRequest request, CancellationToken cancellationToken)
    {
      var sortDefinition = UsersOrderDefinitionResolver.FromModel(request.Sort);

      var users = await fUserRepository.GetSortedUsersAsync(request.Filter, sortDefinition, cancellationToken);

      return new UserPreviewsModel
      {
        UserPreviews = users.ToModel(),
        UserCount = await fUserRepository.GetUserCountAsync(request.Filter, cancellationToken)
      };
    }
  }
}
