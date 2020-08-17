using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shamyr.Cloud.Authority.Service.Models.ConnectToken;
using Shamyr.Cloud.Authority.Service.Notifications.CurrentUser;
using Shamyr.Cloud.Authority.Service.Requests.ConnectToken;

namespace Shamyr.Cloud.Authority.Service.Controllers.V1
{
  /// <summary>
  /// Controller for connection tokens
  /// </summary>
  [ApiController]
  [Route("api/v1/connect/token", Name = "Connect token")]
  public class ConnectTokenController: ControllerBase
  {
    private readonly IMediator fMediator;

    public ConnectTokenController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Logs user out
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <response code="204">User logged out</response>
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<NoContentResult> DeleteAsync(CancellationToken cancellationToken)
    {
      await fMediator.Send(new LogoutRequest(), cancellationToken);
      await fMediator.Publish(new LoggedOutNotification(), cancellationToken);

      return NoContent();
    }

    /// <summary>
    /// Signs user in using password grant type
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">User logged in</response>
    /// <response code="400">Model is invalid, username is invalid or password is invalid</response>
    [HttpPost("password")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TokensModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<TokensModel> PostPasswordLoginAsync([FromBody] PasswordLoginPostModel model, CancellationToken cancellationToken)
    {
      return fMediator.Send(new PostPasswordLoginRequest(model), cancellationToken);
    }

    /// <summary>
    /// Signs user in using refresh grant type
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">User's token refreshed</response>
    /// <response code="400">Model is invalid, refresh token is invalid.</response>
    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TokensModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<TokensModel> PutRefreshLoginAsync([FromBody] RefreshLoginPostModel model, CancellationToken cancellationToken)
    {
      return fMediator.Send(new PostRefreshLoginRequest(model), cancellationToken);
    }
  }
}
