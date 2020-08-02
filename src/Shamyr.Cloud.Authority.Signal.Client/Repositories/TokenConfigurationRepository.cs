﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Token.Models;
using Shamyr.Extensions.DependencyInjection;
using Shamyr.Threading;

namespace Shamyr.Cloud.Authority.Client.Repositories
{
  [Singleton]
  public class TokenConfigurationRepository: ITokenConfigurationRepository
  {
    private readonly LinkedList<TaskCompletionSource<TokenConfigurationModel>> fConfigurationSources = new LinkedList<TaskCompletionSource<TokenConfigurationModel>>();

    private TokenConfigurationModel? fConfiguration;


    public async Task<TokenConfigurationModel> GetAsync(CancellationToken cancellationToken)
    {
      TaskCompletionSource<TokenConfigurationModel>? source = null;
      try
      {
        lock (fConfigurationSources)
        {
          if (fConfiguration != null)
            return fConfiguration;

          source = new TaskCompletionSource<TokenConfigurationModel>(TaskCreationOptions.RunContinuationsAsynchronously);
          fConfigurationSources.AddLast(source);
        }

        return await source.WaitAsync(TimeSpan.FromSeconds(10), cancellationToken);
      }
      finally
      {
        if (source != null)
          lock (fConfigurationSources)
            fConfigurationSources.Remove(source);
      }
    }

    public bool IsSet()
    {
      lock (fConfigurationSources)
        return fConfiguration != null;
    }

    public void Set(TokenConfigurationModel configuration)
    {
      if (configuration is null)
        throw new ArgumentNullException(nameof(configuration));

      lock (fConfigurationSources)
      {
        fConfiguration = configuration;

        foreach (var source in fConfigurationSources)
          source.SetResult(configuration);

      }
    }
  }
}
