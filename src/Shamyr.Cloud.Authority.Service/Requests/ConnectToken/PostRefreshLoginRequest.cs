using MediatR;
using Shamyr.Cloud.Authority.Service.Models.ConnectToken;

namespace Shamyr.Cloud.Authority.Service.Requests.ConnectToken
{
  public class PostRefreshLoginRequest: IRequest<TokensModel>
  {
    public RefreshLoginPostModel Model { get; }

    public PostRefreshLoginRequest(RefreshLoginPostModel model)
    {
      Model = model;
    }
  }
}
