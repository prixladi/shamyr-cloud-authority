using MediatR;
using Shamyr.Cloud.Authority.Service.Models.Token;

namespace Shamyr.Cloud.Authority.Service.Requests.Token
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
