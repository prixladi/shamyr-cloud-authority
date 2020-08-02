using MediatR;

namespace Shamyr.Cloud.Authority.Service.Requests.Emails
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
