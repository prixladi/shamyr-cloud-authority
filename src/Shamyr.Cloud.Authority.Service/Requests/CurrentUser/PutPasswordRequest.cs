using MediatR;
using Shamyr.Cloud.Authority.Service.Models.CurrentUser;

namespace Shamyr.Cloud.Authority.Service.Requests.CurrentUser
{
  public class PutPasswordRequest: IRequest
  {
    public PutPasswordModel Model { get; }

    public PutPasswordRequest(PutPasswordModel model)
    {
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
