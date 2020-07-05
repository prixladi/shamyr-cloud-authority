using MediatR;
using MongoDB.Bson;

namespace Shamyr.Cloud.Gateway.Service.Notifications.Users
{
  public class UserVerificationStatusChangedNotification: INotification
  {
    public ObjectId UserId { get; set; }
    public bool Verified { get; set; }

    public UserVerificationStatusChangedNotification(ObjectId userId, bool verified)
    {
      UserId = userId;
      Verified = verified;
    }
  }
}
