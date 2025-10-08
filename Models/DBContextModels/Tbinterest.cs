using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbinterest
{
    public long InterestId { get; set; }

    public string? InterestDesc { get; set; }

    public string? InterestType { get; set; }
}
