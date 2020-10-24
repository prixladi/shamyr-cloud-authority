using MediatR;
using Shamyr.Cloud.Authority.Service.Models.Token;

namespace Shamyr.Cloud.Authority.Service.Requests.Token
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
