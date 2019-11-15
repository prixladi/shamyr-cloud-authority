namespace Shamyr.Server
{
  public class UserDisabledException: StatusException
  {
    public UserDisabledException()
      : base(CustomStatusCodes.Status431UserDisabled, "User is disabled.") { }
  }
}
