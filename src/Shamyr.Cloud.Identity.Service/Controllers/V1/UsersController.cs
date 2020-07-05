using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Shamyr.Cloud.Identity.Service.Extensions;
using Shamyr.Cloud.Identity.Service.Models;
using Shamyr.Cloud.Identity.Service.Repositories;
using Shamyr.Cloud.Identity.Service.Services;

namespace Shamyr.Cloud.Identity.Service.Controllers
{
  [Authorize]
  [Route("api/v1/users")]
  public class UsersController: ControllerBase
  {
    private readonly ITokenService fTokenService;
    private readonly IUserRepository fUserRepository;

    public UsersController(ITokenService tokenService, IUserRepository userRepository)
    {
      fTokenService = tokenService;
      fUserRepository = userRepository;
    }

    /// <summary>
    /// Returts user with provided ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserIdentityProfileModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<UserIdentityProfileModel> GetAsync(ObjectId id, CancellationToken cancellationToken)
    {
      var user = await fUserRepository.GetAsync(id, cancellationToken);
      if (user is null)
        throw new NotFoundException(nameof(user));

      return user.ToModel();
    }

    /// <summary>
    /// Validates provided JWT and returns validation result
    /// </summary>
    /// <param name="token">JWT</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{token}/validated")]
    [ProducesResponseType(typeof(UserIdentityValidationModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<UserIdentityValidationModel> GetAsync(string token, CancellationToken cancellationToken)
    {
      var (result, message) = await fTokenService.ValidateTokenAsync(token, cancellationToken);

      return new UserIdentityValidationModel
      {
        Result = result,
        User = message?.ToModel()
      };
    }
  }
}
