using MediatR;

namespace Shamyr.Cloud.Gateway.Service.Requests.Emails
{
  public class PatchPasswordResetRequest: IRequest
  {
    public string Email { get; }

    public PatchPasswordResetRequest(string email)
    {
      Email = email;
    }
  }
}
