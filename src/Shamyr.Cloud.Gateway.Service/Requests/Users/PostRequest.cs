using MediatR;
using Shamyr.Cloud.Gateway.Service.Models;
using Shamyr.Cloud.Gateway.Service.Models.Users;

namespace Shamyr.Cloud.Gateway.Service.Requests.Users
{
  public class PostRequest: IRequest<IdModel>
  {
    public UserPostModel Model { get; }

    public PostRequest(UserPostModel model)
    {
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
