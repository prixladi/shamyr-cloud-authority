using MediatR;
using Shamyr.Cloud.Authority.Service.Models.Logins;

namespace Shamyr.Cloud.Authority.Service.Requests.Logins
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
