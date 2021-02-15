using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Notifications.CurrentUser;
using Shamyr.Cloud.Authority.Service.Services;
using Shamyr.Cloud.Authority.Service.Services.Identity;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.ExtensibleLogging;

namespace Shamyr.Cloud.Authority.Service.Handlers.Notifications
{
  public class CurrentUserNotificationHandler: INotificationHandler<LoggedOutNotification>
  {
    private readonly IIdentityService fIdentityService;
    private readonly IClientHubService fBrodacastHubService;
    private readonly ILoggingContextService fLoggingContextService;

    public CurrentUserNotificationHandler(
      IIdentityService identityService,
      IClientHubService broadcastHubService,
      ILoggingContextService loggingContextService)
    {
      fIdentityService = identityService;
      fBrodacastHubService = broadcastHubService;
      fLoggingContextService = loggingContextService;
    }

    public Task Handle(LoggedOutNotification notification, CancellationToken cancellationToken)
    {
      var context = fLoggingContextService.GetRequestContext();
      var @event = new UserLoggedOutEvent(fIdentityService.Current.UserId.ToString(), context);
      const string method = nameof(IRemoteClient.UserLoggedOutEventAsync);
      return fBrodacastHubService.SendEventAsync(@event, method, context, cancellationToken);
    }
  }
}
