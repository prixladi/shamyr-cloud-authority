using MediatR;
using Shamyr.Cloud.Authority.Service.Models;
using Shamyr.Cloud.Authority.Service.Models.Users;

namespace Shamyr.Cloud.Authority.Service.Requests.Users
{
  public class PostRequest: IRequest<IdModel>
  {
    public PostModel Model { get; }

    public PostRequest(PostModel model)
    {
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
