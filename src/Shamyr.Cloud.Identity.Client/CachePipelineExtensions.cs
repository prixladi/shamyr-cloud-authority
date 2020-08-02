using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Identity.Client.Services;

namespace Shamyr.Cloud.Identity.Client
{
  internal static class CachePipelineExtensions
  {
    public static IEnumerable<IUserCacheService> GetServices(this CachePipeline pipeline, IServiceProvider provider)
    {
      if (pipeline is null)
        throw new ArgumentNullException(nameof(pipeline));
      if (provider is null)
        throw new ArgumentNullException(nameof(provider));

      foreach (var pipelinePart in pipeline)
        yield return (IUserCacheService)provider.GetRequiredService(pipelinePart);
    }
  }
}
