using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Shamyr.Base64;
using Shamyr.Cloud.Identity.Client.Extensions;
using Shamyr.Cloud.Identity.Client.Models;
using Shamyr.Cloud.Identity.Service.Protos;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client
{
  public class IdentityGrpcClient: IIdentityRemoteClient
  {
    private readonly IIdentityRemoteClientConfig fIdentityClientConfig;
    private readonly ITracker fTracker;

    public IdentityGrpcClient(IIdentityRemoteClientConfig identityClientConfig, ITracker tracker)
    {
      fIdentityClientConfig = identityClientConfig;
      fTracker = tracker;
    }

    public async Task<UserIdentityValidationModel> GetIdentityByJwtAsync(IOperationContext context, string token, CancellationToken cancellationToken)
    {
      using (fTracker.TrackDependency(context, "GRPC", "Indentity service call", RoleNames._IdentityService, $"Token: {token}", out var dependecyContext))
      {
        if (fIdentityClientConfig.IdentityUrl.Scheme == "http")
          AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

        var channel = GrpcChannel.ForAddress(fIdentityClientConfig.IdentityUrl);
        var client = new Users.UsersClient(channel);

        try
        {
          var result = await client.GetByTokenAsync(new GetByTokenRequest { Token = token }, GetMetadata());
          dependecyContext.Success();
          return new UserIdentityValidationModel
          {
            Result = result.IdentityResult.ToModel(),
            User = result.ProfileMessage.ToModel()
          };
        }
        catch (BadRequestException ex)
        {
          dependecyContext.Fail();
          fTracker.TrackException(dependecyContext, ex);
          throw new IdentityException("Unsuccessfull indetity service call.", StatusCodes.Status401Unauthorized, ex);
        }
        catch (Exception ex)
        {
          dependecyContext.Fail();
          fTracker.TrackException(dependecyContext, ex);
          throw new IdentityException("Unsuccessfull indetity service call.", StatusCodes.Status500InternalServerError, ex);
        }
      }
    }

    public async Task<UserIdentityProfileModel?> GetUserByIdAsync(IOperationContext context, string userId, CancellationToken cancellationToken)
    {
      using (fTracker.TrackDependency(context, "REST", "Indentity service call", RoleNames._IdentityService, $"ID: {userId}", out var dependecyContext))
      {
        var channel = GrpcChannel.ForAddress(fIdentityClientConfig.IdentityUrl);
        var client = new Users.UsersClient(channel);

        try
        {
          var result = await client.GetByIdAsync(new GetByIdRequest { UserId = userId }, GetMetadata());
          dependecyContext.Success();
          return result.ProfileMessage.ToModel();
        }
        catch (NotFoundException)
        {
          dependecyContext.Fail();
          return null;
        }
        catch (Exception ex)
        {
          dependecyContext.Fail();
          fTracker.TrackException(dependecyContext, ex);
          throw new IdentityException("Unsuccessfull indetity service call.", StatusCodes.Status500InternalServerError, ex);
        }
      }
    }

    private Metadata GetMetadata()
    {
      var credentials = new Base64String($"{fIdentityClientConfig.ClientId}:{fIdentityClientConfig.ClientSecret}");
      return new Metadata
      {
        { "Authorization", $"Basic {credentials.ToEncodedString()}" }
      };
    }
  }
}
