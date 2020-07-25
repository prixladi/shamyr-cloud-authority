using System;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Gateway.Token.Models;
using Shamyr.Text.Json;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client.Services
{
  public class TokenConfigurationService: ITokenConfigurationService
  {
    private readonly ITracker fTracker;

    public TokenConfigurationService(ITracker tracker)
    {
      fTracker = tracker;
    }

    public async Task<TokenConfigurationModel?> TryGetAsync(Uri gatewayUrl, IOperationContext context, CancellationToken cancellationToken)
    {
      using (fTracker.TrackDependency(context, "REST", "Indentity service call", RoleNames._GatewayService, gatewayUrl.ToString(), out var dependecyContext))
      {
        var url = gatewayUrl.AppendPath("configuration.json");
        var client = HttpClientContext.Client;

        try
        {
          var result = await client.GetAsync(url);
          result.EnsureSuccessStatusCode();
          dependecyContext.Success();

          var body = await result.Content.ReadAsStringAsync(cancellationToken);
          return await JsonConvert.DeserializeAsync<TokenConfigurationModel>(body, cancellationToken);
        }
        catch (Exception ex)
        {
          dependecyContext.Fail();
          fTracker.TrackException(dependecyContext, ex);
          return null;
        }
      }
    }
  }
}
