using Shamyr.Cloud.Gateway.Service.Emails;

namespace Shamyr.Cloud.Gateway.Service.Factories
{
  public interface IEmailBuilderFactory
  {
    IEmailBuilder? TryCreate(IEmailBuildContext context);
  }
}