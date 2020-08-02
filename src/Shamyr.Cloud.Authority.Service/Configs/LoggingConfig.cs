﻿using Microsoft.Extensions.Logging;

namespace Shamyr.Cloud.Authority.Service.Configs
{
  public static class LoggingConfig
  {
    public static void Setup(ILoggingBuilder builder)
    {
      builder.AddConsole();
    }
  }
}
