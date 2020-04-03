using Shamyr.AspNetCore.Handlers.Exceptions;

namespace Shamyr.Server.Handlers.Exceptions
{
  public class UserDisabledExceptionHandler: ExceptionHandlerBase<UserDisabledException>
  {
    protected override int DoGetStatusCode(UserDisabledException exception)
    {
      return CustomStatusCodes.Status431UserDisabled;
    }
  }
}
