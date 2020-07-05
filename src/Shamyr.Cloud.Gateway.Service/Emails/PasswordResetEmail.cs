using System;
using System.Net.Mail;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Emails;

namespace Shamyr.Cloud.Gateway.Service.Emails
{
  public class PasswordResetEmail: EmailBase
  {
    private const string _PasswordTokenMark = "**PASSWORD_TOKEN**";
    private const string _UserIdMark = "**USER_ID**";
    private const string _UsernameMark = "**USERNAME**";

    private readonly ObjectId fUserId;
    private readonly string fPasswordToken;
    private readonly string fUsername;

    public static PasswordResetEmail New(UserDoc user)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      var recipient = new MailAddress(user.Email);
      return new PasswordResetEmail(recipient, user.Id, user.Username, user.PasswordToken);
    }

    private PasswordResetEmail(MailAddress recipientAddress, ObjectId userId, string username, string? passwordToken)
      : base(recipientAddress, EnvironmentUtils.GatewayUrl, EnvironmentUtils.PortalUrl)
    {
      fUserId = userId;
      fUsername = username ?? throw new ArgumentNullException(nameof(username));
      fPasswordToken = passwordToken ?? throw new ArgumentNullException(nameof(passwordToken));
    }

    public override string Subject => EmailTemplates.PasswordResetEmailSubject;

    public override string Body =>
     UseBaseTransformation(EmailTemplates.PasswordResetEmailTemplate)
          .Replace(_PasswordTokenMark, fPasswordToken)
          .Replace(_UsernameMark, fUsername)
          .Replace(_UserIdMark, fUserId.ToString());
  }
}
