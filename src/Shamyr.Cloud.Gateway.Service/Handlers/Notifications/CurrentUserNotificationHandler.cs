using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.AspNetCore.Services;
using Shamyr.Cloud.Gateway.Service.Notifications.CurrentUser;
using Shamyr.Cloud.Gateway.Service.Services;
using Shamyr.Cloud.Gateway.Service.Services.Identity;
using Shamyr.Cloud.Gateway.Signal.Messages;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Notifications
{
  public class CurrentUserNotificationHandler: INotificationHandler<CurrentUserLoggedOutNotification>
  {
    private readonly IIdentityService fIdentityService;
    private readonly IClientHubService fBrodacastHubService;
    private readonly ITelemetryService fTelemetryService;

    public CurrentUserNotificationHandler(
      IIdentityService identityService,
      IClientHubService broadcastHubService,
      ITelemetryService telemetryService)
    {
      fIdentityService = identityService;
      fBrodacastHubService = broadcastHubService;
      fTelemetryService = telemetryService;
    }

    public Task Handle(CurrentUserLoggedOutNotification notification, CancellationToken cancellationToken)
    {
      var context = fTelemetryService.GetRequestContext();
      var @event = new UserLoggedOutEvent(fIdentityService.Current.UserId.ToString(), context);
      var method = nameof(IRemoteClient.UserLoggedOutEventAsync);
      return fBrodacastHubService.SendEventAsync(context, @event, method, cancellationToken);
    }
  }
}
