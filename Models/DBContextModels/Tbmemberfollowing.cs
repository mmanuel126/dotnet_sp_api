using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbmemberfollowing
{
    public long Id { get; set; }

    public long? MemberId { get; set; }

    public long? FollowingMemberId { get; set; }
}
