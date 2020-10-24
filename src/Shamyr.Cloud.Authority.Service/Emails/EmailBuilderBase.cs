using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Service.Emails
{
  public abstract class EmailBuilderBase<TContext>: IEmailBuilder
    where TContext : IEmailBuildContext
  {
    private const string _AuthorityUrlMark = "{{AUTHORITY_URL}}";
    private const string _PortalUrlMark = "{{PORTAL_URL}}";

    private readonly IEmailTemplateRepository fTemplateRepository;
    private readonly ILogger fLogger;

    protected abstract IEnumerable<(string, Func<TContext, string>)> BodyReplaceRules { get; }
    protected abstract IEnumerable<(string, Func<TContext, string>)> SubjectReplaceRules { get; }

    protected EmailBuilderBase(IEmailTemplateRepository templateRepository, ILogger logger)
    {
      fTemplateRepository = templateRepository;
      fLogger = logger;
    }

    public bool CanBuild(IEmailBuildContext context)
    {
      return context is TContext;
    }

    public async Task<EmailDto?> TryBuildAsync(IEmailBuildContext context, CancellationToken cancellationToken)
    {
      if (context.EmailTemplateId is null)
        return null;

      var template = await fTemplateRepository.GetAsync(context.EmailTemplateId.Value, cancellationToken);
      if (template is null)
        throw new InvalidOperationException($"Email template with ID '{context.EmailTemplateId.Value}' does not exists.");

      return new EmailDto
      (
        RecipientAddress: GetMailAddress((TContext)context),
        Body: BuildBody((TContext)context, template.Body),
        Subject: BuildSubject((TContext)context, template.Subject),
        IsBodyHtml: template.IsHtml
      );
    }

    private string BuildBody(TContext context, string body)
    {
      var content = body;

      if (context.Client.AuthorityUrl is not null)
        content = content.Replace(_AuthorityUrlMark, context.Client.AuthorityUrl);
      else
        fLogger.LogWarning(context, $"Client with ID '{context.Client.Id}' doesn't have authority URL set.");

      if (context.Client.PortalUrl is not null)
        content = content.Replace(_PortalUrlMark, context.Client.PortalUrl);
      else
        fLogger.LogWarning(context, $"Client with ID '{context.Client.Id}' doesn't have portal URL set.");

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
