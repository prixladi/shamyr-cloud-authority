using Microsoft.AspNetCore.Hosting;
using Shamyr.Cloud.Authority.Service;

await new WebHostBuilder()
  .UseKestrel()
  .UseStartup<Startup>()
  .Build()
  .RunAsync();