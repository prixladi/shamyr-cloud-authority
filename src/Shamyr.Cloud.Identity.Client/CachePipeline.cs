using System;
using System.Collections.Generic;

namespace Shamyr.Cloud.Identity.Client
{
  public class CachePipeline
  {
    private readonly LinkedList<Type> fServiceTypes;

    public int Length => fServiceTypes.Count;
     
    public CachePipeline()
    {
      fServiceTypes = new LinkedList<Type>();
    }

    public IEnumerator<Type> GetEnumerator()
    {
      return fServiceTypes.GetEnumerator();
    }

    public CachePipeline UseCache<T>() where T : class
    {
      fServiceTypes.AddLast(typeof(T));
      return this;
    }
  }
}
