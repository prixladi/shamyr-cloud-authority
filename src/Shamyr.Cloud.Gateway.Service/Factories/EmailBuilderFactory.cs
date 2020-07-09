using System;
using System.Linq;
using Shamyr.Cloud.Gateway.Service.Emails;
using Shamyr.DependencyInjection;

namespace Shamyr.Cloud.Gateway.Service.Factories
{
  public class EmailBuilderFactory: FactoryBase<IEmailBuilder>, IEmailBuilderFactory
  {
    public EmailBuilderFactory(IServiceProvider serviceProvider)
      : base(serviceProvider) { }

    public IEmailBuilder? TryCreate(IEmailBuildContext context)
    {
      return GetComponents().SingleOrDefault(x => x.CanBuild(context));
    }
  }
}
