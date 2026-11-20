using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class ChatbotLicense
{
    public int CustomerId { get; set; }

    public int TotalQues { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime Validity { get; set; }

    public int ConsumeQues { get; set; }

    public bool? Status { get; set; }

    public int? LicenseId { get; set; }
}
