using MediatR;
using Shamyr.Cloud.Gateway.Service.Models;

namespace Shamyr.Cloud.Gateway.Service.Requests.Emails
{
  public class GetVerificationRequest: IRequest<IdModel>
  {
    public string EmailToken { get; }
    public string Email { get; }

    public GetVerificationRequest(string email, string emailToken)
    {
      EmailToken = emailToken ?? throw new System.ArgumentNullException(nameof(emailToken));
      Email = email ?? throw new System.ArgumentNullException(nameof(email));
    }
  }
}
