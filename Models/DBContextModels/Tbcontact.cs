using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbcontact
{
    public long MemberId { get; set; }

    public long ContactId { get; set; }

    public long Status { get; set; }

    public DateTime? Datestamp { get; set; }

    public virtual Tbmember Contact { get; set; } = null!;

    public virtual Tbmember Member { get; set; } = null!;
}
