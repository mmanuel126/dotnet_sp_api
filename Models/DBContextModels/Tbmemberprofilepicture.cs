using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbmemberprofilepicture
{
    public long ProfileId { get; set; }

    public long MemberId { get; set; }

    public string? FileName { get; set; }

    public decimal? IsDefault { get; set; }

    public decimal? Removed { get; set; }
}
