namespace Shamyr.Cloud
{
  public class UserDisabledException: StatusException
  {
    public UserDisabledException()
      : base(CustomStatusCodes._Status431UserDisabled, "User is disabled.") { }
  }
}
