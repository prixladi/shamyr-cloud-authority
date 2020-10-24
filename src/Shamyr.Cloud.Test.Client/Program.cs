using Microsoft.AspNetCore.Hosting;
using Shamyr.Cloud.Identity.Client.Test;

await new WebHostBuilder()
  .UseKestrel()
  .UseStartup<Startup>()
  .Build()
  .RunAsync();