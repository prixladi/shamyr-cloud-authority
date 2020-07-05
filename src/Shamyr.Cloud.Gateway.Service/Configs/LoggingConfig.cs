using Microsoft.Extensions.Logging;

namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public static class LoggingConfig
  {
    public static void Setup(ILoggingBuilder builder)
    {
      builder.AddConsole();
    }
  }
}
