using Shamyr.AspNetCore.Handlers.Exceptions;

namespace Shamyr.Cloud.Handlers.Exceptions
{
  public class UserDisabledExceptionHandler: ExceptionHandlerBase<UserDisabledException>
  {
    protected override int DoGetStatusCode(UserDisabledException exception)
    {
      return CustomStatusCodes.Status431UserDisabled;
    }
  }
}
