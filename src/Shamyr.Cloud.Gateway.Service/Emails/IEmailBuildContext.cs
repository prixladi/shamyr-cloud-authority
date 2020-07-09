using Shamyr.Cloud.Database.Documents;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Service.Emails
{
  public interface IEmailBuildContext: IOperationContext
  {
    EmailTemplateType EmailType { get; }
  }
}
