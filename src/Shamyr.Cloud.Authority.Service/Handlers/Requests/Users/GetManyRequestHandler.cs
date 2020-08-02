using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Models.Users;
using Shamyr.Cloud.Authority.Service.OrderDefinitions;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Users;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Users
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
        UserPreviews = users.ToPreview(),
        UserCount = await fUserRepository.GetUserCountAsync(request.Filter, cancellationToken)
      };
    }
  }
}
