using MediatR;
using Shamyr.Cloud.Authority.Service.Models;

namespace Shamyr.Cloud.Authority.Service.Requests.Emails
{
  public class PutVerifiedRequest: IRequest<IdModel>
  {
    public string EmailToken { get; }
    public string Email { get; }

    public PutVerifiedRequest(string email, string emailToken)
    {
      EmailToken = emailToken ?? throw new System.ArgumentNullException(nameof(emailToken));
      Email = email ?? throw new System.ArgumentNullException(nameof(email));
    }
  }
}
