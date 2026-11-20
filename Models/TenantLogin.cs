using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class TenantLogin
{
    public Guid CaptchaId { get; set; }

    public string CaptchaCode { get; set; } = null!;

    public DateTime GeneratedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool IsUsed { get; set; }
}
