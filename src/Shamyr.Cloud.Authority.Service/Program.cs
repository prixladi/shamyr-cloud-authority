using Microsoft.AspNetCore.Hosting;
using Serilog;
using Shamyr.Cloud.Authority.Service;
using Shamyr.Cloud.Authority.Service.Configs;

await new WebHostBuilder()
  .UseSerilog(LoggingConfig.Setup)
  .UseKestrel()
  .UseStartup<Startup>()
  .Build()
  .RunAsync();
