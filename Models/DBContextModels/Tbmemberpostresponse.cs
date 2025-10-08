using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbmemberpostresponse
{
    public long PostResponseId { get; set; }

    public long? PostId { get; set; }

    public string? Description { get; set; }

    public DateTime? ResponseDate { get; set; }

    public long? MemberId { get; set; }

    public virtual Tbmember? Member { get; set; }

    public virtual Tbmemberpost? Post { get; set; }
}
