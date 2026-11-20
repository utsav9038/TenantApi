using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class SchedulerJobState
{
    public string TenantKey { get; set; } = null!;

    public string StepName { get; set; } = null!;

    public string GroupName { get; set; } = null!;

    public string Lane { get; set; } = null!;

    public int SortOrder { get; set; }

    public int LockMinutes { get; set; }

    public bool WindowOnly { get; set; }

    public DateTime NextDueUtc { get; set; }

    public DateTime? LastSuccessUtc { get; set; }

    public DateTime? LastAttemptUtc { get; set; }

    public int FailCount { get; set; }

    public string Status { get; set; } = null!;

    public string? LockedBy { get; set; }

    public DateTime? LockUntilUtc { get; set; }

    public string? LastError { get; set; }
}
