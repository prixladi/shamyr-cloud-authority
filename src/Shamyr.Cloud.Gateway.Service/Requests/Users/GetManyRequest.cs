using MediatR;
using Shamyr.Cloud.Gateway.Service.Models.Users;

namespace Shamyr.Cloud.Gateway.Service.Requests.Users
{
  public class GetManyRequest: IRequest<UserPreviewsModel>
  {
    public UserQueryFilter Filter { get; }
    public UserSortModel Sort { get; }

    public GetManyRequest(UserQueryFilter filter, UserSortModel sort)
    {
      Filter = filter ?? throw new System.ArgumentNullException(nameof(filter));
      Sort = sort ?? throw new System.ArgumentNullException(nameof(sort));
    }
  }
}
