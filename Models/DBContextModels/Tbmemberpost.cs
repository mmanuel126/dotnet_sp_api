using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbmemberpost
{
    public long PostId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? PostDate { get; set; }

    public string? AttachFile { get; set; }

    public long? MemberId { get; set; }

    public long? FileType { get; set; }

    public long? LikeCounter { get; set; }

    public virtual Tbmember? Member { get; set; }

    public virtual ICollection<Tbmemberpostresponse> Tbmemberpostresponses { get; set; } = new List<Tbmemberpostresponse>();
}
