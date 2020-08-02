using MediatR;
using Shamyr.Cloud.Authority.Service.Models.Users;

namespace Shamyr.Cloud.Authority.Service.Requests.CurrentUser
{
  public class GetRequest: IRequest<UserDetailModel>
  {
  }
}
