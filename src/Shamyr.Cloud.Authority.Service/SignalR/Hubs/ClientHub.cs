using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Services;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Service.SignalR.Hubs
{
  public partial class ClientHub: Hub, IRemoteServer
  {
    private readonly IClientService fClientService;
    private readonly ILogger fLogger;

    public ClientHub(IClientService clientService, ILogger logger)
    {
      fClientService = clientService;
      fLogger = logger;
    }

    public override async Task OnConnectedAsync()
    {
      fLogger.LogInformation(LoggingContext.Root, $"SignalR client with Connection Id '{Context.ConnectionId}' connected.");
      await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
      if (exception is not null)
        fLogger.LogException(LoggingContext.Root, exception, $"SignalR client with Connection Id '{Context.ConnectionId}' disconnected.");
      else
        fLogger.LogInformation(LoggingContext.Root, $"SignalR client with Connection Id '{Context.ConnectionId}' disconnected.");

      return base.OnDisconnectedAsync(exception);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
      if (!ObjectId.TryParse(request.ClientId, out var clientId))
        throw new BadRequestException("ClientId has invalid format.");

      var result = await fClientService.LoginAsync(clientId, request.ClientSecret, Context.ConnectionAborted);
      if (result == ClientLoginStatus.Ok)
      {
        var connection = new Connection(clientId);
        Context.Items.Add(typeof(Connection), connection);
      }

      return result switch
      {
        ClientLoginStatus.Ok => new LoginResponse(request.GetContext()),
        ClientLoginStatus.ClientNotFound => throw new UnauthorizedException($"Client with Id '{request.ClientId}' not found."),
        ClientLoginStatus.SecretNotSet => throw new UnauthorizedException($"Client does not have secret set."),
        ClientLoginStatus.InvalidSecret => throw new UnauthorizedException($"Invalid client secret."),
        _ => throw new NotImplementedException(result.ToString()),
      };
    }

    public async Task<SubscribeEventsResponse> SubscribeResourceAsync(SubscribeEventsRequest request)
    {
      if (!Context.Items.TryGetValue(typeof(Connection), out var value) || value is not Connection connection || connection.ClientId is null)
        throw new UnauthorizedException("Client is unauthorized.");

      await fClientService.SubscribeToResourcesAsync(request.Resources, Context.ConnectionId, Context.ConnectionAborted);

      return new SubscribeEventsResponse(request.GetContext());
    }
  }
}
