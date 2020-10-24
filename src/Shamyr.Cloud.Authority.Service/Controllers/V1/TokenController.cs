using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shamyr.Cloud.Authority.Models;
using Shamyr.Cloud.Authority.Service.Models.Token;
using Shamyr.Cloud.Authority.Service.Notifications.CurrentUser;
using Shamyr.Cloud.Authority.Service.Requests.Token;

namespace Shamyr.Cloud.Authority.Service.Controllers.V1
{
  [ApiController]
  [Route("api/v1/token")]
  public class TokenController: ControllerBase
  {
    private readonly IMediator fMediator;

    public TokenController(IMediator mediator)
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
    /// Gets info about token configuration
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Token configuration model</returns>
    [HttpGet("configuration")]
    public async Task<TokenConfigurationModel> GetConfigurationAsync(CancellationToken cancellationToken)
    {
      return await fMediator.Send(new GetConfigurationRequest(), cancellationToken);
    }

    /// <summary>
    /// Signs user in using Password grant type
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
    /// Signs user in using Google grant type
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">User's token refreshed</response>
    /// <response code="400">Model is invalid, refresh token is invalid.</response>
    [HttpPost("google")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TokensModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<TokensModel> PutGoogleLoginAsync([FromBody] GoogleLoginPostModel model, CancellationToken cancellationToken)
    {
      return fMediator.Send(new PostGoogleLoginRequest(model), cancellationToken);
    }

    /// <summary>
    /// Signs user in using Refresh grant type
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
