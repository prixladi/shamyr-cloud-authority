using Microsoft.AspNetCore.Hosting;
using Shamyr.Cloud.Identity.Service;

await new WebHostBuilder()
  .UseKestrel()
  .UseStartup<Startup>()
  .Build()
  .RunAsync();