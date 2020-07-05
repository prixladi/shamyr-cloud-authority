using System;
using System.Net.Mail;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Emails;

namespace Shamyr.Cloud.Gateway.Service.Emails
{
  public class VerifyAccountEmail: EmailBase
  {
    private const string _VerifyTokenMark = "**VERIFY_TOKEN**";
    private const string _EmailMark = "**EMAIL**";

    private readonly string fEmail;
    private readonly string? fVerfifyToken;

    public static VerifyAccountEmail New(UserDoc user)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      var recipient = new MailAddress(user.Email);
      return new VerifyAccountEmail(recipient, user.Email, user.EmailToken);
    }

    private VerifyAccountEmail(MailAddress recipientAddress, string email, string? verfifyToken)
      : base(recipientAddress, EnvironmentUtils.GatewayUrl, EnvironmentUtils.PortalUrl)
    {
      fEmail = email ?? throw new ArgumentNullException(nameof(email));
      fVerfifyToken = verfifyToken ?? throw new ArgumentNullException(nameof(verfifyToken));
    }

    public override string Subject => EmailTemplates.VerifyAccountEmailSubject;

    public override string Body =>
     UseBaseTransformation(EmailTemplates.VerifyAccountEmailTemplate)
          .Replace(_VerifyTokenMark, fVerfifyToken)
          .Replace(_EmailMark, fEmail);
  }
}
