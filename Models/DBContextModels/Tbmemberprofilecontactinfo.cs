using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbmemberprofilecontactinfo
{
    public long MemberId { get; set; }

    public string? Email { get; set; }

    public decimal? ShowEmailToMembers { get; set; }

    public string? OtherEmail { get; set; }

    public string? Facebook { get; set; }

    public string? Instagram { get; set; }

    public string? CellPhone { get; set; }

    public decimal? ShowCellPhone { get; set; }

    public string? HomePhone { get; set; }

    public decimal? ShowHomePhone { get; set; }

    public string? OtherPhone { get; set; }

    public string? Address { get; set; }

    public decimal? ShowAddress { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Zip { get; set; }

    public string? Twitter { get; set; }

    public string? Website { get; set; }

    public string? Neighborhood { get; set; }

    public virtual Tbmember Member { get; set; } = null!;
}
