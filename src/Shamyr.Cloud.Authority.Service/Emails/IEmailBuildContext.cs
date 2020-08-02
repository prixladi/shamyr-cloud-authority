using Shamyr.Cloud.Database.Documents;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Service.Emails
{
  public interface IEmailBuildContext: IOperationContext
  {
    EmailTemplateType EmailType { get; }
  }
}
