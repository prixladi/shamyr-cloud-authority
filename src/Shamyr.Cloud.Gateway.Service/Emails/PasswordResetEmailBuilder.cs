using System;
using System.Collections.Generic;
using System.Net.Mail;
using Shamyr.Cloud.Gateway.Service.Repositories;

namespace Shamyr.Cloud.Gateway.Service.Emails
{
  using ReplaceRule = ValueTuple<string, Func<PasswordResetEmailContext, string>>;

  public class PasswordResetEmailBuilder: EmailBuilderBase<PasswordResetEmailContext>
  {
    private const string _PasswordTokenMark = "{{PASSWORD_TOKEN}}";
    private const string _UserIdMark = "{{USER_ID}}";
    private const string _UsernameMark = "{{USERNAME}}";

    protected override IEnumerable<ReplaceRule> BodyReplaceRules
    {
      get
      {
        yield return (_PasswordTokenMark, x => x.PasswordToken!);
        yield return (_UserIdMark, x => x.Id.ToString());
        yield return (_UsernameMark, x => x.Username);
      }
    }

    protected override IEnumerable<ReplaceRule> SubjectReplaceRules => Array.Empty<ReplaceRule>();

    public PasswordResetEmailBuilder(IEmailTemplateRepository templateRepository)
      : base(templateRepository) { }

    protected override MailAddress GetMailAddress(PasswordResetEmailContext context)
    {
      return new MailAddress(context.Email);
    }
  }
}
