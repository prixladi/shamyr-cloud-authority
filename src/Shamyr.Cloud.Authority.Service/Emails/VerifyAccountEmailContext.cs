using System;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Service.Emails
{
  public class VerifyAccountEmailContext: LoggingContext, IEmailBuildContext
  {
    public EmailTemplateType EmailType => EmailTemplateType.VerifyAccount;

    public string EmailToken { get; }
    public string Email { get; }

    public static VerifyAccountEmailContext New(UserDoc user, ILoggingContext context)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      return new VerifyAccountEmailContext(user.EmailToken!, user.Email, context);
    }

    public VerifyAccountEmailContext(string emailToken, string email, ILoggingContext context)
      : base(context)
    {
      EmailToken = emailToken ?? throw new ArgumentNullException(nameof(emailToken));
      Email = email ?? throw new ArgumentNullException(nameof(email));
    }
  }
}
