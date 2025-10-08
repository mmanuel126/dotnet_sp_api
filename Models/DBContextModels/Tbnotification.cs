using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbnotification
{
    public long NotificationId { get; set; }

    public string? Subject { get; set; }

    public string? Notification { get; set; }

    public DateTime? SentDate { get; set; }

    public decimal? Status { get; set; }
}
