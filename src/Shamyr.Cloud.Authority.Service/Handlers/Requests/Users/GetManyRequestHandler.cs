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
  public class GetManyRequestHandler: IRequestHandler<GetManyRequest, PreviewsModel>
  {
    private readonly IUserRepository fUserRepository;

    public GetManyRequestHandler(IUserRepository userRepository)
    {
      fUserRepository = userRepository;
    }

    public async Task<PreviewsModel> Handle(GetManyRequest request, CancellationToken cancellationToken)
    {
      var sortDefinition = UsersOrderDefinitionResolver.FromModel(request.Sort);
      var filter = request.Filter.ToDto();

      var users = await fUserRepository.GetAsync(filter, sortDefinition, cancellationToken);

      return new PreviewsModel
      {
        UserPreviews = users.ToPreview(),
        UserCount = await fUserRepository.GetUserCountAsync(filter, cancellationToken)
      };
    }
  }
}
