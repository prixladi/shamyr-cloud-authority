using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shamyr.Cloud.Identity.Client.Test
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
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
