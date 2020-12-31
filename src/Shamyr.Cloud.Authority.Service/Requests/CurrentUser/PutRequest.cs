using MediatR;
using Shamyr.Cloud.Authority.Service.Models.Users;

namespace Shamyr.Cloud.Authority.Service.Requests.CurrentUser
{
  public class PutRequest: IRequest
  {
    public PutModel Model { get; }

    public PutRequest(PutModel model)
    {
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
