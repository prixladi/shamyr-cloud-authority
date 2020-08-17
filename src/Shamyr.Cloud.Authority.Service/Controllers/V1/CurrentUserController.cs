using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shamyr.Cloud.Authority.Service.Models.CurrentUser;
using Shamyr.Cloud.Authority.Service.Models.Users;
using Shamyr.Cloud.Authority.Service.Notifications.CurrentUser;
using Shamyr.Cloud.Authority.Service.Requests.ConnectToken;
using Shamyr.Cloud.Authority.Service.Requests.CurrentUser;

namespace Shamyr.Cloud.Authority.Service.Controllers.V1
{
  /// <summary>
  /// Controller for manipulating with currently logged user
  /// </summary>
  [Authorize]
  [ApiController]
  [Route("api/v1/users/current", Name = "Current user")]
  public class CurrrentUserController: ControllerBase
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
    /// Creates password for user if he doesn't have any
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">Password changed</response>
    /// <response code="400">Invalid model or invalid old password</response>
    /// <response code="409">User already has password, use PUT /current/user/password</response>
    [HttpPost("password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<NoContentResult> PostPasswordAsync([FromBody] CurrentUserPutPasswordModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PutPasswordRequest(model), cancellationToken);
      await fMediator.Send(new LogoutRequest(), cancellationToken);
      await fMediator.Publish(new LoggedOutNotification(), cancellationToken);

      return NoContent();
    }

    /// <summary>
    /// Changes current user password
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">Password changed</response>
    /// <response code="400">Invalid model or invalid old password</response>
    /// <response code="409">User does't have password set, use POST /current/user/password</response>
    [HttpPut("password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<NoContentResult> PutPasswordAsync([FromBody] CurrentUserPutPasswordModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PutPasswordRequest(model), cancellationToken);
      await fMediator.Send(new LogoutRequest(), cancellationToken);
      await fMediator.Publish(new LoggedOutNotification(), cancellationToken);

      return NoContent();
    }
  }
}
