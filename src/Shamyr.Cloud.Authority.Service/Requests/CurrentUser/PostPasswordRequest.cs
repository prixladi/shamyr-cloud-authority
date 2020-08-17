using MediatR;
using Shamyr.Cloud.Authority.Service.Models.CurrentUser;

namespace Shamyr.Cloud.Authority.Service.Requests.CurrentUser
{
  public class PostPasswordRequest: IRequest
  {
    public CurrentUserPostPasswordModel Model { get; }

    public PostPasswordRequest(CurrentUserPostPasswordModel model)
    {
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
