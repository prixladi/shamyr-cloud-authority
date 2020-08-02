using System;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Identity.Client.Factories;

namespace Shamyr.Cloud.Identity.Client
{
  public class IdentityServicesBuilder
  {
    private readonly IServiceCollection fServices;

    internal IdentityServicesBuilder(IServiceCollection services)
    {
      fServices = services ?? throw new ArgumentNullException(nameof(services));
    }

    public IdentityCacheBuilder AddIdentityCaching<TCachePipelineFactory>()
      where TCachePipelineFactory : class, ICachePipelineFactory
    {
      fServices.AddTransient<ICachePipelineFactory, TCachePipelineFactory>();
      return new IdentityCacheBuilder(fServices);
    }
  }
}
