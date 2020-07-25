using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Client.Services;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Client
{
  internal class CachePipelineManager: IAsyncDisposable
  {
    private class Result
    {
      public Result(UserModel user, string userId)
      {
        User = user ?? throw new ArgumentNullException(nameof(user));
        UserId = userId ?? throw new ArgumentNullException(nameof(userId));
      }

      public UserModel User { get; }
      public string UserId { get; }
    }

    private readonly IUserCacheService[] fServices;
    private readonly CancellationToken fPipelineCancellation;

    private Stack<IUserCacheService>? fUsedServices;
    private Result? fResult;

    public CachePipelineManager(IUserCacheService[] services, CancellationToken pipelineCancellation)
    {
      fServices = services ?? throw new ArgumentNullException(nameof(services));
      fPipelineCancellation = pipelineCancellation;
    }

    public async Task<UserModel?> TryGetCachedUserAsync(string userId)
    {
      if (userId is null)
        throw new ArgumentNullException(nameof(userId));

      fUsedServices = new Stack<IUserCacheService>(fServices.Count());

      foreach (var service in fServices)
      {
        var cachedUser = await service.RetrieveUserAsync(userId, fPipelineCancellation);
        if (cachedUser != null)
          return cachedUser;

        fUsedServices.Push(service);
      }

      return null;
    }

    public void SetResult(UserModel user, string userId)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      if (userId is null)
        throw new ArgumentNullException(nameof(userId));

      fResult = new Result(user, userId);
    }

    public async ValueTask DisposeAsync()
    {
      if (fResult is null || fUsedServices is null)
        return;

      foreach (var service in fUsedServices)
        await service.SaveUserAsync(fResult.UserId, fResult.User, fPipelineCancellation);
    }
  }
}
