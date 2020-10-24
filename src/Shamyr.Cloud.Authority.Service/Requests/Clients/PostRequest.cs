using MediatR;
using Shamyr.Cloud.Authority.Service.Models;
using Shamyr.Cloud.Authority.Service.Models.Clients;

namespace Shamyr.Cloud.Authority.Service.Requests.Clients
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
