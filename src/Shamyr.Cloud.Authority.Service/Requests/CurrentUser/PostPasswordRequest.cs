using MediatR;
using Shamyr.Cloud.Authority.Service.Models.CurrentUser;

namespace Shamyr.Cloud.Authority.Service.Requests.CurrentUser
{
  public class PostPasswordRequest: IRequest
  {
    public PostPasswordModel Model { get; }

    public PostPasswordRequest(PostPasswordModel model)
    {
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
