using MediatR;
using Shamyr.Cloud.Gateway.Service.Models.CurrentUser;

namespace Shamyr.Cloud.Gateway.Service.Requests.CurrentUser
{
  public class PatchPasswordRequest: IRequest
  {
    public CurrentUserPutPasswordModel Model { get; }

    public PatchPasswordRequest(CurrentUserPutPasswordModel model)
    {
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
