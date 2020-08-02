using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Service.Repositories;

namespace Shamyr.Cloud.Authority.Service.Emails
{
  public abstract class EmailBuilderBase<TContext>: IEmailBuilder
    where TContext : IEmailBuildContext
  {
    private readonly IEmailTemplateRepository fTemplateRepository;

    protected abstract IEnumerable<(string, Func<TContext, string>)> BodyReplaceRules { get; }
    protected abstract IEnumerable<(string, Func<TContext, string>)> SubjectReplaceRules { get; }

    protected EmailBuilderBase(IEmailTemplateRepository templateRepository)
    {
      fTemplateRepository = templateRepository;
    }

    public bool CanBuild(IEmailBuildContext context)
    {
      return context is TContext;
    }

    public async Task<EmailDto?> TryBuildAsync(IEmailBuildContext context, CancellationToken cancellationToken)
    {
      var template = await fTemplateRepository.GetByTypeAsync(context.EmailType, cancellationToken);
      if (template == null)
        return null;

      return new EmailDto
      {
        RecipientAddress = GetMailAddress((TContext)context),
        Body = BuildBody((TContext)context, template.Body),
        Subject = BuildSubject((TContext)context, template.Subject),
        IsBodyHtml = template.IsHtml
      };
    }

    private string BuildBody(TContext context, string body)
    {
      var content = body;
      foreach (var (mark, selector) in BodyReplaceRules)
        content = content.Replace(mark, selector(context));

      return content;
    }

    private string BuildSubject(TContext context, string subject)
    {
      var content = subject;
      foreach (var (mark, selector) in SubjectReplaceRules)
        content = content.Replace(mark, selector(context));

      return content;
    }

    protected abstract MailAddress GetMailAddress(TContext context);
  }
}
