using MediatR;
using Shamyr.Cloud.Authority.Service.Models;

namespace Shamyr.Cloud.Authority.Service.Requests.Emails
{
  public class PatchVerificationRequest: IRequest
  {
    public string Email { get; }
    public ClientIdModel Model { get; }

    public PatchVerificationRequest(string email, ClientIdModel model)
    {
      Email = email;
      Model = model;
    }
  }
}
