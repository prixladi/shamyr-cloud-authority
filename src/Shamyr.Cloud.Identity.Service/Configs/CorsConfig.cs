﻿using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Shamyr.Cloud.Identity.Service.Configs
{
  internal static class CorsConfig
  {
    public static void Setup(CorsPolicyBuilder builder)
    {
      builder.AllowAnyHeader();
      builder.AllowAnyMethod();
      builder.AllowAnyOrigin();
    }
  }
}