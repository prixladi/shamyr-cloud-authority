using MediatR;
using Shamyr.Cloud.Authority.Service.Models;

namespace Shamyr.Cloud.Authority.Service.Requests.Emails
{
  public class PatchPasswordResetRequest: IRequest
  {
    public string Email { get; }
    public ClientIdModel Model { get; }

    public PatchPasswordResetRequest(string email, ClientIdModel model)
    {
      Email = email;
      Model = model;
    }
  }
}
