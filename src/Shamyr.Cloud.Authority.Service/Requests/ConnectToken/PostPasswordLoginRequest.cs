using MediatR;
using Shamyr.Cloud.Authority.Service.Models.ConnectToken;

namespace Shamyr.Cloud.Authority.Service.Requests.ConnectToken
{
  public class PostPasswordLoginRequest: IRequest<TokensModel>
  {
    public PasswordLoginPostModel Model { get; }

    public PostPasswordLoginRequest(PasswordLoginPostModel model)
    {
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
