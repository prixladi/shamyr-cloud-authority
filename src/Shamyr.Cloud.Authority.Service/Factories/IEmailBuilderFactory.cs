using Shamyr.Cloud.Authority.Service.Emails;

namespace Shamyr.Cloud.Authority.Service.Factories
{
  public interface IEmailBuilderFactory
  {
    IEmailBuilder? TryCreate(IEmailBuildContext context);
  }
}