﻿using System.Net.Mail;
using System.Threading.Tasks;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Service.Emails
{
  public interface IEmailClient
  {
    Task SendEmailAsync(MailAddress recipient, string subject, EmailBody body, IOperationContext context);
  }
}