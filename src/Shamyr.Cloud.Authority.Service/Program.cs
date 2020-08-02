using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Shamyr.Cloud.Authority.Service
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
