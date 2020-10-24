using System;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Service.Emails
{
  public class VerifyAccountEmailContext: LoggingContext, IEmailBuildContext
  {
    public EmailTemplateType EmailType => EmailTemplateType.VerifyAccount;

    public string EmailToken { get; }
    public string Email { get; }
    public ClientDoc Client { get; }
    public ObjectId? EmailTemplateId => Client.VerifyAccountEmailTemplateId;

    public static VerifyAccountEmailContext New(UserDoc user, ClientDoc client, ILoggingContext context)
    {
      return new VerifyAccountEmailContext(
        user.EmailToken!, 
        user.Email, 
        client, 
        context);
    }

    public VerifyAccountEmailContext(string emailToken, string email, ClientDoc client, ILoggingContext context)
      : base(context)
    {
      EmailToken = emailToken ?? throw new ArgumentNullException(nameof(emailToken));
      Email = email ?? throw new ArgumentNullException(nameof(email));
      Client = client ?? throw new ArgumentNullException(nameof(client));
    }
  }
}
