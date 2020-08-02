using System;

namespace Shamyr.Cloud.Authority.Service.Emails
{
  public class EmailBody
  {
    public string Content { get; }
    public bool IsHtml { get; }

    public EmailBody(string content, bool isHtml)
    {
      Content = content ?? throw new ArgumentNullException(nameof(content));
      IsHtml = isHtml;
    }
  }
}
