using MediatR;
using MongoDB.Bson;

namespace Shamyr.Cloud.Gateway.Service.Notifications.Users
{
  public class UserLoggedOutNotification: INotification
  {
    public ObjectId UserId { get; }

    public UserLoggedOutNotification(ObjectId userId)
    {
      UserId = userId;
    }
  }
}
