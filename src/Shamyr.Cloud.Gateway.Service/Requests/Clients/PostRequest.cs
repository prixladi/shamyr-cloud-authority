using MediatR;
using Shamyr.Cloud.Gateway.Service.Models;
using Shamyr.Cloud.Gateway.Service.Models.Clients;

namespace Shamyr.Cloud.Gateway.Service.Requests.Clients
{
  public class PostRequest: IRequest<IdModel>
  {
    public ClientPostModel Model { get; }

    public PostRequest(ClientPostModel model)
    {
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
