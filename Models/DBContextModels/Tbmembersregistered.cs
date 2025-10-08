using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbmembersregistered
{
    public long MemberCodeId { get; set; }

    public long MemberId { get; set; }

    public DateTime? RegisteredDate { get; set; }

    public virtual Tbmember Member { get; set; } = null!;
}
