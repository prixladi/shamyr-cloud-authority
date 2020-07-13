using System.Net.Http;

namespace Shamyr.Cloud.Gateway.Service
{
  public static class HttpClientContext
  {
    public static HttpClient Client { get; } = new HttpClient();
  }
}
