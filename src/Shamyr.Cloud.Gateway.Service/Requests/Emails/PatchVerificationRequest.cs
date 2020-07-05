using MediatR;

namespace Shamyr.Cloud.Gateway.Service.Requests.Emails
{
  public class PatchVerificationRequest: IRequest
  {
    public string Email { get; }

    public PatchVerificationRequest(string email)
    {
      Email = email;
    }
  }
}
