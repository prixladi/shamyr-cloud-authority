using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Gateway.Service.Emails;
using Shamyr.DependencyInjection;
using Shamyr.Emails;

namespace Shamyr.Cloud.Gateway.Service.IoC
{
  public static class Emails
  {
    public static void AddEmails(this IServiceCollection services)
    {
      services.AddTransient<IEmailClient, EmailClient>();

      using var scan = services.CreateScanner<Startup>();
      scan.AddAllTypesOf<IEmailBuilder>();
    }
  }
}
