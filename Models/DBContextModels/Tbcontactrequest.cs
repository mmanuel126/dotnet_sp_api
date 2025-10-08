using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbcontactrequest
{
    public long RequestId { get; set; }

    public long? FromMemberId { get; set; }

    public long? ToMemberId { get; set; }

    public DateTime? RequestDate { get; set; }

    public long? Status { get; set; }

    public virtual Tbmember? FromMember { get; set; }

    public virtual Tbmember? ToMember { get; set; }
}
