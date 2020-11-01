using System;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Service.Emails
{
  public class PasswordResetEmailContext: LoggingContext, IEmailBuildContext
  {
    public EmailTemplateType EmailType => EmailTemplateType.PasswordReset;

    public string PasswordToken { get; }
    public ObjectId UserId { get; }
    public string? Username { get; }
    public string Email { get; }
    public ClientDoc Client { get; }
    public ObjectId? EmailTemplateId => Client.PasswordResetEmailTemplateId;

    public static PasswordResetEmailContext New(UserDoc user, ClientDoc client, ILoggingContext context)
    {
      return new PasswordResetEmailContext(
        user.PasswordToken!,
        user.Id,
        user.Username,
        user.Email,
        client,
        context);
    }

    public PasswordResetEmailContext(string passwordToken, ObjectId userId, string? username, string email, ClientDoc client, ILoggingContext context)
      : base(context)
    {
      PasswordToken = passwordToken ?? throw new ArgumentNullException(nameof(passwordToken));
      UserId = userId;
      Username = username;
      Email = email ?? throw new ArgumentNullException(nameof(email));
      Client = client ?? throw new ArgumentNullException(nameof(client));
    }
  }
}
