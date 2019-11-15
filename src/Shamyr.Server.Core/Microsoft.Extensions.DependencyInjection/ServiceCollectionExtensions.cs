using Shamyr.AspNetCore.Handlers.Exceptions;
using Shamyr.Server.Handlers.Exceptions;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ServiceCollectionExtensions
  {
    public static void AddCustomExceptionHandling(this IServiceCollection services)
    {
      services.AddTransient<IExceptionHandler, UserNotVerifiedExceptionHandler>();
    }
  }
}
