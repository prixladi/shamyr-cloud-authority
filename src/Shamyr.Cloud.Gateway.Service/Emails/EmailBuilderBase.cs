using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Repositories;

namespace Shamyr.Cloud.Gateway.Service.Emails
{
  public abstract class EmailBuilderBase<TContext>: IEmailBuilder
    where TContext : IEmailBuildContext
  {
    private readonly IEmailTemplateRepository fTemplateRepository;

    protected abstract (string, Func<TContext, string>)[] BodyReplaceRules { get; }
    protected abstract (string, Func<TContext, string>)[] SubjectReplaceRules { get; }

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
        Body = BuildBody((TContext)context, template.Body, cancellationToken),
        Subject = BuildSubject((TContext)context, template.Subject, cancellationToken),
        IsBodyHtml = template.IsHtml
      };
    }

    private string BuildBody(TContext context, string body, CancellationToken cancellationToken)
    {
      var content = body;
      foreach (var (mark, selector) in BodyReplaceRules)
        content = content.Replace(mark, selector(context));

      return content;
    }

    private string BuildSubject(TContext context, string subject, CancellationToken cancellationToken)
    {
      var content = subject;
      foreach (var (mark, selector) in SubjectReplaceRules)
        content = content.Replace(mark, selector(context));

      return content;
    }

    protected abstract MailAddress GetMailAddress(TContext context);
  }
}
