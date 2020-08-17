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
    public string Username { get; }
    public string Email { get; }

    public static PasswordResetEmailContext New(UserDoc user, ILoggingContext context)
    {
      return new PasswordResetEmailContext(user.PasswordToken!, user.Id, user.Username, user.Email, context);
    }

    public PasswordResetEmailContext(string passwordToken, ObjectId userId, string username, string email, ILoggingContext context)
      : base(context)
    {
      PasswordToken = passwordToken ?? throw new ArgumentNullException(nameof(passwordToken));
      UserId = userId;
      Username = username ?? throw new ArgumentNullException(nameof(username));
      Email = email ?? throw new ArgumentNullException(nameof(email));
    }
  }
}
