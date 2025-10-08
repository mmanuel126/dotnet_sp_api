using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbmemberprofileeducationv2
{
    public long MemberId { get; set; }

    public long SchoolId { get; set; }

    public long SchoolType { get; set; }

    public string? SchoolName { get; set; }

    public string? ClassYear { get; set; }

    public string? Major { get; set; }

    public long? DegreeType { get; set; }

    public string? Societies { get; set; }

    public string? Description { get; set; }

    public string? SportLevelType { get; set; }
}
