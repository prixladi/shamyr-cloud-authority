using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.AspNetCore.Services;
using Shamyr.Cloud.Authority.Service.Notifications.CurrentUser;
using Shamyr.Cloud.Authority.Service.Services;
using Shamyr.Cloud.Authority.Service.Services.Identity;
using Shamyr.Cloud.Authority.Signal.Messages;

namespace Shamyr.Cloud.Authority.Service.Handlers.Notifications
{
  public class CurrentUserNotificationHandler: INotificationHandler<LoggedOutNotification>
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

    public Task Handle(LoggedOutNotification notification, CancellationToken cancellationToken)
    {
      var context = fTelemetryService.GetRequestContext();
      var @event = new UserLoggedOutEvent(fIdentityService.Current.UserId.ToString(), context);
      var method = nameof(IRemoteClient.UserLoggedOutEventAsync);
      return fBrodacastHubService.SendEventAsync( @event, method, context, cancellationToken);
    }
  }
}
