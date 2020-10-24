using System;
using System.Collections.Generic;
using System.Net.Mail;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Service.Emails
{
  using ReplaceRule = ValueTuple<string, Func<VerifyAccountEmailContext, string>>;

  public class VerifyAccountEmailBuilder: EmailBuilderBase<VerifyAccountEmailContext>
  {
    private const string _VerifyTokenMark = "{{VERIFY_TOKEN}}";
    private const string _EmailMark = "{{EMAIL}}";

    protected override IEnumerable<ReplaceRule> BodyReplaceRules
    {
      get
      {
        yield return (_VerifyTokenMark, x => x.EmailToken!);
        yield return (_EmailMark, x => x.Email);
      }
    }

    protected override IEnumerable<ReplaceRule> SubjectReplaceRules => Array.Empty<ReplaceRule>();

    public VerifyAccountEmailBuilder(IEmailTemplateRepository templateRepository, ILogger logger)
      : base(templateRepository, logger) { }

    protected override MailAddress GetMailAddress(VerifyAccountEmailContext context)
    {
      return new MailAddress(context.Email);
    }
  }
}
