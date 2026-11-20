using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class RevokedToken
{
    public string TokenHash { get; set; } = null!;

    public DateTime ExpiresAtUtc { get; set; }

    public DateTime RevokedAtUtc { get; set; }
}
