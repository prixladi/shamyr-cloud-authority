using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Repositories.Clients;
using Shamyr.Cloud.Gateway.Service.Services;
using Shamyr.Cloud.Gateway.Signal.Messages;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Service.SignalR.Hubs
{
  public partial class ClientHub: Hub, IRemoteServer
  {
    private readonly IClientConnectionRepository fClientConnectionRepository;
    private readonly IClientService fClientService;
    private readonly ITracker fTracker;

    public ClientHub(
      IClientConnectionRepository clientConnectionRepository,
      IClientService clientService,
      ITracker tracker)
    {
      fClientConnectionRepository = clientConnectionRepository;
      fClientService = clientService;
      fTracker = tracker;
    }

    public override async Task OnConnectedAsync()
    {
      fTracker.TrackInformation(OperationContext.Origin, $"SignalR client with Connection Id '{Context.ConnectionId}' connected.");
      var connection = new Connection(Context.ConnectionId);
      fClientConnectionRepository.AddOrUpdate(connection);

      await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
      if (exception != null)
        fTracker.TrackException(OperationContext.Origin, exception, $"SignalR client with Connection Id '{Context.ConnectionId}' disconnected.");
      else
        fTracker.TrackInformation(OperationContext.Origin, $"SignalR client with Connection Id '{Context.ConnectionId}' disconnected.");

      fClientConnectionRepository.Remove(Context.ConnectionId);

      return base.OnDisconnectedAsync(exception);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
      if (!ObjectId.TryParse(request.ClientId, out var clientId))
        throw new BadRequestException("ClientId has invalid format.");

      var result = await fClientService.LoginAsync(clientId, request.ClientSecret, Context.ConnectionAborted);

      if (result == ClientLoginStatus.Ok)
      {
        var connection = new Connection(Context.ConnectionId, clientId);
        fClientConnectionRepository.AddOrUpdate(connection);
      }

      return result switch
      {
        ClientLoginStatus.Ok => new LoginResponse(request.GetContext()),
        ClientLoginStatus.ClientNotFound => throw new UnauthorizedException($"Client with Id '{request.ClientId}' not found."),
        ClientLoginStatus.InvalidSecret => throw new UnauthorizedException($"Invalid client secret."),
        _ => throw new NotImplementedException($"Login result '{result} is not implemented."),
      };
    }

    public async Task<SubscribeIdentityResourcesResponse> SubscribeResourceAsync(SubscribeIdentityResourcesRequest request)
    {
      var connection = fClientConnectionRepository.Get(Context.ConnectionId);
      if (connection is null || connection.ClientId is null)
        throw new UnauthorizedException("Client is unauthorized.");

      await fClientService.SubscribeToResourcesAsync(request.Resources, Context.ConnectionId, Context.ConnectionAborted);

      return new SubscribeIdentityResourcesResponse(request.GetContext());
    }
  }
}
