﻿using System;

namespace Shamyr.Cloud
{
  public class EmailNotVerifiedException: Exception
  {
    public object EmailPrincipal { get; }
    public int StatusCode { get; }

    public EmailNotVerifiedException(object emailPrincipal)
      : base("Email was not verified.")
    {
      StatusCode = CustomStatusCodes._Status430NotVerified;
      EmailPrincipal = emailPrincipal ?? throw new ArgumentNullException(nameof(emailPrincipal));
    }
  }
}
