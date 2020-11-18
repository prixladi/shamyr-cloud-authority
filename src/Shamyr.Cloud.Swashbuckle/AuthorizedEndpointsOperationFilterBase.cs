using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Shamyr.Cloud.Swashbuckle
{
  public abstract class AuthorizedEndpointsOperationFilterBase: AspNetCore.Swashbuckle.Filters.AuthorizedEndpointsOperationFilterBase
  {
    protected override IEnumerable<(int statusCode, string description)> GetStatuses()
    {
      yield return (StatusCodes.Status401Unauthorized, "User is not authorized.");
      yield return (CustomStatusCodes._Status430NotVerified, "Account email is not verified.");
      yield return (CustomStatusCodes._Status431UserDisabled, "User is disabled.");
    }
  }
}
