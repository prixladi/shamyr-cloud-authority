using System;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Service.Emails
{
  public class PasswordResetEmailContext: OperationContext, IEmailBuildContext
  {
    public EmailTemplateType EmailType => EmailTemplateType.PasswordReset;

    public string PasswordToken { get; }
    public ObjectId UserId { get; }
    public string Username { get; }
    public string Email { get; }

    public static PasswordResetEmailContext New(IOperationContext context, UserDoc user)
    {
      return new PasswordResetEmailContext(context, user.PasswordToken!, user.Id, user.Username, user.Email);
    }

    public PasswordResetEmailContext(IOperationContext context, string passwordToken, ObjectId userId, string username, string email)
      : base(context)
    {
      PasswordToken = passwordToken ?? throw new ArgumentNullException(nameof(passwordToken));
      UserId = userId;
      Username = username ?? throw new ArgumentNullException(nameof(username));
      Email = email ?? throw new ArgumentNullException(nameof(email));
    }
  }
}
