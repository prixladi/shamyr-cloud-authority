using System.Net.Http;

namespace Shamyr.Cloud.Authority.Service
{
  public static class HttpClientContext
  {
    public static HttpClient Client { get; } = new HttpClient();
  }
}
