using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shamyr.Cloud.Authority.Service.Models.CurrentUser;
using Shamyr.Cloud.Authority.Service.Models.Users;
using Shamyr.Cloud.Authority.Service.Notifications.CurrentUser;
using Shamyr.Cloud.Authority.Service.Requests.CurrentUser;
using Shamyr.Cloud.Authority.Service.Requests.Token;

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
    private readonly ISender fSender;
    private readonly IPublisher fPublisher;

    public CurrrentUserController(ISender sender, IPublisher publisher)
    {
      fSender = sender;
      fPublisher = publisher;
    }

    /// <summary>
    /// Returns info about currently logged user
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Returns user model with informations about current user</response>
    [HttpGet]
    [ProducesResponseType(typeof(DetailModel), StatusCodes.Status200OK)]
    public async Task<DetailModel> GetAsync(CancellationToken cancellationToken)
    {
      return await fSender.Send(new GetRequest { }, cancellationToken);
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
    public async Task<NoContentResult> PostPasswordAsync([FromBody] PostPasswordModel model, CancellationToken cancellationToken)
    {
      await fSender.Send(new PostPasswordRequest(model), cancellationToken);
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
    public async Task<NoContentResult> PutPasswordAsync([FromBody] PutPasswordModel model, CancellationToken cancellationToken)
    {
      await fSender.Send(new PutPasswordRequest(model), cancellationToken);
      await fSender.Send(new LogoutRequest(), cancellationToken);
      await fPublisher.Publish(new LoggedOutNotification(), cancellationToken);

      return NoContent();
    }
  }
}
