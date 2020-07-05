using MediatR;
using Shamyr.Cloud.Gateway.Service.Models.Users;

namespace Shamyr.Cloud.Gateway.Service.Requests.CurrentUser
{
  public class GetRequest: IRequest<UserDetailModel>
  {
  }
}
