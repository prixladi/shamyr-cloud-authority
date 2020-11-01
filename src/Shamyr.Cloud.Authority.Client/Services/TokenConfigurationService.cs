using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Models;
using Shamyr.Logging;
using Shamyr.Text.Json;

namespace Shamyr.Cloud.Authority.Client.Services
{
  internal class TokenConfigurationService: ITokenConfigurationService
  {
    public async Task<TokenConfigurationModel> GetAsync(Uri authorityUrl, ILoggingContext context, CancellationToken cancellationToken)
    {
      var url = authorityUrl.AppendPath("api/v1/token/configuration");
      var client = HttpClientContext.Client;
      var result = await client.GetAsync(url, cancellationToken);
      result.EnsureSuccessStatusCode();

      var body = await result.Content.ReadAsStringAsync(cancellationToken);
      return await JsonConvert.DeserializeAsync<TokenConfigurationModel>(body, new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      }, cancellationToken);
    }
  }
}
