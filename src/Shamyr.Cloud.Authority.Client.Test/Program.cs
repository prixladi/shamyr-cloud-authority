using Microsoft.AspNetCore.Hosting;
using Shamyr.Cloud.Authority.Client.Test;

await new WebHostBuilder()
  .UseKestrel()
  .UseStartup<Startup>()
  .Build()
  .RunAsync();
