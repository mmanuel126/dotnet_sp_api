using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbmembernotification
{
    public long NotificationMemberId { get; set; }

    public long MemberId { get; set; }

    public long NotificationId { get; set; }
}
