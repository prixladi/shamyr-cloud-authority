﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Notifications.Users;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Services;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.ExtensibleLogging;

namespace Shamyr.Cloud.Authority.Service.Handlers.Notifications
{
  public class UserNotificationHandler:
    INotificationHandler<DisabledChangedNotification>,
    INotificationHandler<LoggedOutNotification>,
    INotificationHandler<VerificationChangedNotification>,
    INotificationHandler<AdminChangedNotification>
  {
    private readonly IClientHubService fClientHubService;
    private readonly IUserRepository fUserRepository;
    private readonly ILoggingContextService fLoggingContextService;

    public UserNotificationHandler(
      IClientHubService clientHubService,
      IUserRepository userRepository,
      ILoggingContextService loggingContextService)
    {
      fClientHubService = clientHubService;
      fUserRepository = userRepository;
      fLoggingContextService = loggingContextService;
    }

    public async Task Handle(DisabledChangedNotification notification, CancellationToken cancellationToken)
    {
      var context = fLoggingContextService.GetRequestContext();
      var @event = new UserDisabledChangedEvent(notification.UserId.ToString(), notification.Disabled, context);
      const string method = nameof(IRemoteClient.UserLoggedOutEventAsync);
      await fClientHubService.SendEventAsync(@event, method, context, cancellationToken);
    }

    public async Task Handle(LoggedOutNotification notification, CancellationToken cancellationToken)
    {
      var context = fLoggingContextService.GetRequestContext();
      var @event = new UserLoggedOutEvent(notification.UserId.ToString(), context);
      const string method = nameof(IRemoteClient.UserLoggedOutEventAsync);
      await fClientHubService.SendEventAsync(@event, method, context, cancellationToken);
    }

    public Task Handle(VerificationChangedNotification notification, CancellationToken cancellationToken)
    {
      var context = fLoggingContextService.GetRequestContext();
      var @event = new UserVerifiedChangedEvent(notification.UserId.ToString(), notification.Verified, context);
      const string method = nameof(IRemoteClient.UserVerifiedChangedEventAsync);
      return fClientHubService.SendEventAsync(@event, method, context, cancellationToken);
    }

    public async Task Handle(AdminChangedNotification notification, CancellationToken cancellationToken)
    {
      var context = fLoggingContextService.GetRequestContext();
      var @event = new UserAdminChangedEvent(notification.UserId.ToString(), notification.Admin, context);
      const string method = nameof(IRemoteClient.UserLoggedOutEventAsync);
      await fClientHubService.SendEventAsync(@event, method, context, cancellationToken);
    }
  }
}
