using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Shamyr.Cloud.Identity.Client.Test
{
  public sealed class Program
  {
    public static Task Main()
    {
      return new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseStartup<Startup>()
        .Build()
        .RunAsync();
    }
  }
}
