using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Service.Emails
{
  public interface IEmailBuildContext: ILoggingContext
  {
    EmailTemplateType EmailType { get; }
    ObjectId? EmailTemplateId { get; }
    ClientDoc Client { get; }
  }
}
