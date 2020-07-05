using Shamyr.AspNetCore.Handlers.Exceptions;
using Shamyr.Cloud.Handlers.Exceptions;
using Shamyr.Cloud.Services;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ServiceCollectionExtensions
  {
    public static void AddCustomExceptionHandling(this IServiceCollection services)
    {
      services.AddTransient<IExceptionHandler, UserNotVerifiedExceptionHandler>();
      services.AddTransient<IExceptionHandler, UserDisabledExceptionHandler>();
    }

    public static void AddSecretService(this IServiceCollection services)
    {
      services.AddTransient<ISecretService, SecretService>();
    }
  }
}
