using MediatR;
using Shamyr.Cloud.Authority.Service.Models.Token;

namespace Shamyr.Cloud.Authority.Service.Requests.Token
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
