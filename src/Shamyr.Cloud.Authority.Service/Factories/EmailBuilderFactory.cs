using System;
using System.Linq;
using Shamyr.Cloud.Authority.Service.Emails;
using Shamyr.Extensions.Factories;

namespace Shamyr.Cloud.Authority.Service.Factories
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
