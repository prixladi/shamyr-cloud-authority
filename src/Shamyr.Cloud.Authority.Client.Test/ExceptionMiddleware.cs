using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shamyr.Cloud.Authority.Client.Test
{
  public sealed class ExceptionMiddleware
  {
    private readonly RequestDelegate fNext;

    public ExceptionMiddleware(RequestDelegate next)
    {
      fNext = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await fNext(context);
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
