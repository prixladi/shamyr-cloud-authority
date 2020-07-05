using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Signal.Messages;

namespace Shamyr.Cloud.Gateway.Service.Notifications.Users
{
  public class UserUserPermissionChangedNotification: INotification
  {
    public ObjectId UserId { get; }
    public PermissionKind? PermissionKind { get; }

    public UserUserPermissionChangedNotification(ObjectId userId, PermissionKind? kind)
    {
      UserId = userId;
      PermissionKind = kind;
    }
  }
}
