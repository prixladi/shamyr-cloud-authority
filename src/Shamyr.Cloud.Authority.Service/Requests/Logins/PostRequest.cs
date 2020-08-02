using MediatR;
using Shamyr.Cloud.Authority.Service.Models.Logins;

namespace Shamyr.Cloud.Authority.Service.Requests.Logins
{
  public class PostRequest: IRequest<TokensModel>
  {
    public LoginPostModel Model { get; }

    public PostRequest(LoginPostModel model)
    {
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
