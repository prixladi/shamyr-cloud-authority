using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shamyr.Base64;
using Shamyr.Cloud.Identity.Service.Models;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client
{
  internal class IdentityRestClient: IIdentityClient
  {
    private readonly IIdentityClientConfig fIdentityClientConfig;
    private readonly ITracker fTracker;

    public IdentityRestClient(IIdentityClientConfig identityClientConfig, ITracker tracker)
    {
      fIdentityClientConfig = identityClientConfig;
      fTracker = tracker;
    }

    public async Task<UserModel?> GetUserByIdAsync(string userId, IOperationContext context, CancellationToken cancellationToken)
    {
      string url = GetAbsoluteUrl($"api/v1/users/{userId}");
      using (fTracker.TrackDependency(context, "REST", $"Indentity service call '{url}'", RoleNames._IdentityService, $"ID: {userId}", out var dependecyContext))
      {
        var message = new HttpRequestMessage(HttpMethod.Get, url);
        try
        {
          var model = await CallRestAsync<UserModel>(message, cancellationToken);
          dependecyContext.Success();
          return model;
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

    private async Task<TResponseModel?> CallRestAsync<TResponseModel>(HttpRequestMessage message, CancellationToken cancellationToken)
      where TResponseModel : class
    {
      using var client = HttpClientContext.Client;
      AddAuthHeader(message);
      try
      {
        var response = await client.SendAsync(message, cancellationToken);
        var model = await HandleResponseAsync<TResponseModel>(response, cancellationToken);

        return model;
      }
      catch (Exception ex)
      {
        throw new IdentityException("Unsuccessfull indetity service call.", StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    private async Task<TResponseModel?> HandleResponseAsync<TResponseModel>(HttpResponseMessage response, CancellationToken cancellationToken)
      where TResponseModel : class
    {
      if (!response.IsSuccessStatusCode)
        throw new IdentityException($"Unsuccessful call identity service.", (int)response.StatusCode, response.ReasonPhrase);

      var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
      if (typeof(TResponseModel) == typeof(object) || string.IsNullOrEmpty(responseBody))
        return default;

      return JsonConvert.DeserializeObject<TResponseModel>(responseBody);
    }

    private void AddAuthHeader(HttpRequestMessage message)
    {
      var credentials = new Base64String($"{fIdentityClientConfig.ClientId}:{fIdentityClientConfig.ClientSecret}");
      message.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials.ToEncodedString());
    }

    private string GetAbsoluteUrl(string path)
    {
      return Path.Combine(fIdentityClientConfig.IdentityUrl.ToString(), path);
    }
  }
}
