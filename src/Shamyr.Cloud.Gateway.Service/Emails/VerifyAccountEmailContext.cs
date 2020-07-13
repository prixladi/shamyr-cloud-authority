using System;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Service.Emails
{
  public class VerifyAccountEmailContext: OperationContext, IEmailBuildContext
  {
    public EmailTemplateType EmailType => EmailTemplateType.VerifyAccount;

    public string EmailToken { get; }
    public string Email { get; }

    public static VerifyAccountEmailContext New(UserDoc user, IOperationContext context)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      return new VerifyAccountEmailContext(context, user.EmailToken!, user.Email);
    }

    public VerifyAccountEmailContext(IOperationContext context, string emailToken, string email)
      : base(context)
    {
      EmailToken = emailToken ?? throw new ArgumentNullException(nameof(emailToken));
      Email = email ?? throw new ArgumentNullException(nameof(email));
    }
  }
}
