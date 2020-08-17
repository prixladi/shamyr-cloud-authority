using MediatR;
using Shamyr.Cloud.Authority.Service.Models.ConnectToken;

namespace Shamyr.Cloud.Authority.Service.Requests.ConnectToken
{
  public class PostGoogleLoginRequest: IRequest<TokensModel>
  {
    public GoogleLoginPostModel Model { get; }

    public PostGoogleLoginRequest(GoogleLoginPostModel model)
    {
      Model = model;
    }
  }
}
