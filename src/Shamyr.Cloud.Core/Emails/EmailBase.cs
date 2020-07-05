using System;
using System.Net.Mail;

namespace Shamyr.Cloud.Emails
{
  public abstract class EmailBase
  {
    private const string _GatewayUrlMark = "**GATEWAY_URL**";
    private const string _PortalUrlMark = "**PORTAL_URL**";

    private const string _ContentMark = "**CONTENT**";

    private readonly Uri fGatewayUrl;
    private readonly Uri fPortalUrl;

    public MailAddress RecipientAddress { get; }

    public abstract string Subject { get; }

    public abstract string Body { get; }

    protected EmailBase(
      MailAddress mailAddress,
      Uri gatewayUrl,
      Uri portalUrl)
    {
      RecipientAddress = mailAddress;
      fGatewayUrl = gatewayUrl;
      fPortalUrl = portalUrl;
    }

    protected string UseBaseTransformation(string body)
    {
      if (body is null)
        throw new ArgumentNullException(nameof(body));

      return EmailTemplates.MainEmailTemplate
        .Replace(_ContentMark, body)
        .Replace(_GatewayUrlMark, fGatewayUrl.AbsoluteUri)
        .Replace(_PortalUrlMark, fPortalUrl.AbsoluteUri);
    }
  }
}
