using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.AspNetCore.Services;
using Shamyr.Cloud.Gateway.Service.Notifications.Users;
using Shamyr.Cloud.Gateway.Service.Repositories;
using Shamyr.Cloud.Gateway.Service.Services;
using Shamyr.Cloud.Gateway.Signal.Messages;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Notifications
{
  public class UserNotificationHandler:
    INotificationHandler<UserDisabledStatusChangedNotification>,
    INotificationHandler<UserLoggedOutNotification>,
    INotificationHandler<UserVerificationStatusChangedNotification>
  {
    private readonly IClientHubService fClientHubService;
    private readonly IUserRepository fUserRepository;
    private readonly ITelemetryService fTelemetryService;

    public UserNotificationHandler(
      IClientHubService clientHubService,
      IUserRepository userRepository,
      ITelemetryService telemetryService)
    {
      fClientHubService = clientHubService;
      fUserRepository = userRepository;
      fTelemetryService = telemetryService;
    }

    public async Task Handle(UserDisabledStatusChangedNotification notification, CancellationToken cancellationToken)
    {
      var context = fTelemetryService.GetRequestContext();
      var @event = new UserLoggedOutEvent(notification.UserId.ToString(), context);
      var scewedTime = DateTime.UtcNow.Add(TimeSpan.FromSeconds(-1));

      await Task.WhenAll
      (
        fUserRepository.LogoutAsync(notification.UserId, scewedTime, cancellationToken),
        fClientHubService.SendEventAsync(context, @event, nameof(IRemoteClient.UserLoggedOutEventAsync), cancellationToken)
      );
    }

    public async Task Handle(UserLoggedOutNotification notification, CancellationToken cancellationToken)
    {
      var context = fTelemetryService.GetRequestContext();
      var @event = new UserLoggedOutEvent(notification.UserId.ToString(), context);
      var method = nameof(IRemoteClient.UserLoggedOutEventAsync);
      await fClientHubService.SendEventAsync(context, @event, method, cancellationToken);
    }

    public Task Handle(UserVerificationStatusChangedNotification notification, CancellationToken cancellationToken)
    {
      var context = fTelemetryService.GetRequestContext();
      var @event = new UserVerificationStatusChangedEvent(notification.UserId.ToString(), notification.Verified, context);
      var method = nameof(IRemoteClient.UserVerificationStatusChangedEventAsync);
      return fClientHubService.SendEventAsync(context, @event, method, cancellationToken);
    }
  }
}
