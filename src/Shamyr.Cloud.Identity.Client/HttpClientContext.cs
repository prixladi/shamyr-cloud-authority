using System.Net.Http;

namespace Shamyr.Cloud.Identity.Client
{
  public static class HttpClientContext
  {
    public static HttpClient Client { get; } = new HttpClient();
  }
}
