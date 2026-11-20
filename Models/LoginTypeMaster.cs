using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class LoginTypeMaster
{
    public int LoginTypeId { get; set; }

    public string LoginTypeName { get; set; } = null!;
}
