using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shamyr.Cloud.Identity.Client
{
  internal class CachePipelineManager: IAsyncDisposable
  {
    private class Result
    {
      public Result(CachedUserModel user, string userId)
      {
        User = user ?? throw new ArgumentNullException(nameof(user));
        UserId = userId ?? throw new ArgumentNullException(nameof(userId));
      }

      public Result(CachedUserModel user, string userId, long? expirationInSeconds)
      {
        User = user ?? throw new ArgumentNullException(nameof(user));
        UserId = userId ?? throw new ArgumentNullException(nameof(userId));
        ExpirationInSeconds = expirationInSeconds;
      }

      public CachedUserModel User { get; }
      public string UserId { get; }
      public long? ExpirationInSeconds { get; }
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

    public async Task<CachedUserModel?> TryGetCachedUserAsync(string userId)
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

    public void SetResult(CachedUserModel user, string userId)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      if (userId is null)
        throw new ArgumentNullException(nameof(userId));

      fResult = new Result(user, userId);
    }

    public void SetResult(CachedUserModel user, string userId, long expirationInSeconds)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      if (userId is null)
        throw new ArgumentNullException(nameof(userId));

      fResult = new Result(user, userId, expirationInSeconds);
    }

    public async ValueTask DisposeAsync()
    {
      if (fResult is null || fUsedServices is null)
        return;

      foreach (var service in fUsedServices)
      {
        if (fResult.ExpirationInSeconds.HasValue)
          await service.SaveUserAsync(fResult.UserId, fResult.ExpirationInSeconds.Value, fResult.User, fPipelineCancellation);
        else
          await service.SaveUserAsync(fResult.UserId, fResult.User, fPipelineCancellation);
      }
    }
  }
}
