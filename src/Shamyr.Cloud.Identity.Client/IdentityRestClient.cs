using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shamyr.Base64;
using Shamyr.Cloud.Identity.Client.Models;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client
{
  internal class IdentityRestClient: IIdentityRemoteClient
  {
    private readonly IIdentityRemoteClientConfig fIdentityClientConfig;
    private readonly ITracker fTracker;

    public IdentityRestClient(
      IIdentityRemoteClientConfig identityClientConfig,
      ITracker tracker)
    {
      fIdentityClientConfig = identityClientConfig;
      fTracker = tracker;
    }

    public async Task<UserIdentityValidationModel> GetIdentityByJwtAsync(IOperationContext context, string token, CancellationToken cancellationToken)
    {
      using (fTracker.TrackDependency(context, "REST", "Indentity service call", RoleNames._IdentityService, $"Token: {token}", out var dependecyContext))
      {
        string url = GetAbsoluteUrl($"api/v1/users/{token}/validated");
        try
        {
          var result = await CallRestAsync<UserIdentityValidationModel>(dependecyContext, (client, cancellation) => client.GetAsync(url, cancellation), cancellationToken);

          Debug.Assert(result != null);

          dependecyContext.Success();
          return result;
        }
        catch
        {
          dependecyContext.Fail();
          throw;
        }
      }
    }

    public async Task<UserIdentityProfileModel?> GetUserByIdAsync(IOperationContext context, string userId, CancellationToken cancellationToken)
    {
      using (fTracker.TrackDependency(context, "REST", "Indentity service call", RoleNames._IdentityService, $"ID: {userId}", out var dependecyContext))
      {
        string url = GetAbsoluteUrl($"api/v1/users/{userId}");
        try
        {
          return await CallRestAsync<UserIdentityProfileModel>(dependecyContext, (client, cancellation) => client.GetAsync(url, cancellation), cancellationToken);
        }
        catch (IdentityException ex)
          when (ex.StatusCode == StatusCodes.Status404NotFound)
        {
          dependecyContext.Success();
          return null;
        }
        catch
        {
          dependecyContext.Fail();
          throw;
        }
      }
    }

    private async Task<TResponseModel?> CallRestAsync<TResponseModel>(ITrackingContext context, Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> method, CancellationToken cancellationToken)
      where TResponseModel : class
    {
      using var client = CreateClient();
      try
      {
        var response = await method(client, cancellationToken);
        var model = await HandleResponseAsync<TResponseModel>(response);

        context.Success();

        return model;
      }
      catch (IdentityException)
      {
        context.Fail();
        throw;
      }
      catch (Exception ex)
      {
        context.Fail();
        throw new IdentityException("Unsuccessfull indetity service call.", StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    private async Task<TResponseModel?> HandleResponseAsync<TResponseModel>(HttpResponseMessage response)
      where TResponseModel : class
    {
      if (!response.IsSuccessStatusCode)
        throw new IdentityException($"Unsuccessful call identity service.", (int)response.StatusCode, response.ReasonPhrase);

      var responseBody = await response.Content.ReadAsStringAsync();
      if (typeof(TResponseModel) == typeof(object) || string.IsNullOrEmpty(responseBody))
        return default;

      return JsonConvert.DeserializeObject<TResponseModel>(responseBody);
    }

    private HttpClient CreateClient()
    {
      var client = new HttpClient
      {
        Timeout = TimeSpan.FromSeconds(20)
      };

      var credentials = new Base64String($"{fIdentityClientConfig.ClientId}:{fIdentityClientConfig.ClientSecret}");
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials.ToEncodedString());
      return client;
    }

    private string GetAbsoluteUrl(string path)
    {
      return Path.Combine(fIdentityClientConfig.IdentityUrl.ToString(), path);
    }
  }
}
