using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shamyr.Cloud.Gateway.Service.Models.CurrentUser;
using Shamyr.Cloud.Gateway.Service.Models.Users;
using Shamyr.Cloud.Gateway.Service.Notifications.CurrentUser;
using Shamyr.Cloud.Gateway.Service.Requests.CurrentUser;
using Shamyr.Cloud.Gateway.Service.Requests.Logins;

namespace Shamyr.Cloud.Gateway.Service.Controllers.V1
{
  /// <summary>
  /// Controller for manipulating with currently logged user
  /// </summary>
  [Authorize]
  [Route("api/v1/users/current", Name = "Current user")]
  public class CurrrentUserController: Controller
  {
    private readonly IMediator fMediator;

    public CurrrentUserController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Returns info about currently logged user
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Returns user model with informations about current user</response>
    [HttpGet]
    [ProducesResponseType(typeof(UserDetailModel), StatusCodes.Status200OK)]
    public async Task<UserDetailModel> GetAsync(CancellationToken cancellationToken)
    {
      return await fMediator.Send(new GetRequest { }, cancellationToken);
    }

    /// <summary>
    /// Changes current user password
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">Password changed</response>
    /// <response code="400">Invalid model or invalid old password</response>
    [HttpPatch("password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<NoContentResult> PatchPasswordAsync([FromBody] CurrentUserPutPasswordModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PatchPasswordRequest(model), cancellationToken);
      await fMediator.Send(new DeleteRequest(), cancellationToken);
      await fMediator.Publish(new CurrentUserLoggedOutNotification(), cancellationToken);

      return NoContent();
    }
  }
}
