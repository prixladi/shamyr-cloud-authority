using MediatR;
using Shamyr.Cloud.Authority.Service.Models.Users;

namespace Shamyr.Cloud.Authority.Service.Requests.Users
{
  public class GetManyRequest: IRequest<PreviewsModel>
  {
    public QueryFilter Filter { get; }
    public SortModel Sort { get; }

    public GetManyRequest(QueryFilter filter, SortModel sort)
    {
      Filter = filter ?? throw new System.ArgumentNullException(nameof(filter));
      Sort = sort ?? throw new System.ArgumentNullException(nameof(sort));
    }
  }
}
