using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Service.Emails
{
  public class PasswordResetEmailContext: OperationContext, IEmailBuildContext
  {
    public EmailTemplateType EmailType => EmailTemplateType.PasswordReset;

    public ObjectId UserId { get; }
    public string PasswordToken { get; }
    public string Username { get; }

    public PasswordResetEmailContext(IOperationContext context, ObjectId userId, string passwordToken, string username)
      : base(context)
    {
      UserId = userId;
      PasswordToken = passwordToken;
      Username = username;
    }
  }
}
