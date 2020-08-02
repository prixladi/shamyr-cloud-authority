using Shamyr.Cloud.Identity.Client.Factories;

namespace Shamyr.Cloud.Identity.Client.Test
{
  public class CachePipelineFactory: ICachePipelineFactory
  {
    public CachePipeline Create()
    {
      return new CachePipeline()
        .UseCache<UserCache>();

    }
  }
}
