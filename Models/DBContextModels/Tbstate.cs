using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbstate
{
    public string? Name { get; set; }

    public string? Abbreviation { get; set; }

    public long StateId { get; set; }
}
