using MediatR;

namespace Shamyr.Cloud.Authority.Service.Requests.Emails
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
