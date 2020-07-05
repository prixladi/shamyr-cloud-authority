using MediatR;
using Shamyr.Cloud.Gateway.Service.Models.Logins;

namespace Shamyr.Cloud.Gateway.Service.Requests.Logins
{
  public class PutRefreshRequest: IRequest<TokensModel>
  {
    public LoginPutRefreshModel Model { get; }

    public PutRefreshRequest(LoginPutRefreshModel model)
    {
      Model = model;
    }
  }
}
