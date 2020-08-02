using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Authority.Service.Emails;
using Shamyr.Extensions.DependencyInjection;

namespace Shamyr.Cloud.Authority.Service.IoC
{
  public static class Emails
  {
    public static void AddEmails(this IServiceCollection services)
    {
      services.AddAllTypesOf<IEmailBuilder>();

      services.AddTransient<IEmailClient, EmailClient>();
    }
  }
}
