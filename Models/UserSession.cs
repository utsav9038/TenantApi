using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class UserSession
{
    public string TokenHash { get; set; } = null!;

    public string SubjectKey { get; set; } = null!;

    public DateTime ExpiresAtUtc { get; set; }
}
