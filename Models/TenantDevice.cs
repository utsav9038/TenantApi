using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class TenantDevice
{
    public int Iid { get; set; }

    public string MacAddress { get; set; } = null!;

    public string? Ipaddress { get; set; }

    public string Make { get; set; } = null!;

    public string ControllerType { get; set; } = null!;

    public string DeviceId { get; set; } = null!;

    public int Scpid { get; set; }

    public bool IsController { get; set; }

    public int DeviceStatus { get; set; }

    public long TenantId { get; set; }

    public DateTime CreateDate { get; set; }

    public int GroupId { get; set; }

    public int? Hostid { get; set; }

    public long TenantUserId { get; set; }
}
