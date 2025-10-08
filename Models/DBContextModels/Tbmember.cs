using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbmember
{
    public long MemberId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public long? Status { get; set; }

    public long? SecurityQuestion { get; set; }

    public string? SecurityAnswer { get; set; }

    public long? DeactivateReason { get; set; }

    public string? DeactivateExplanation { get; set; }

    public decimal? FutureEmails { get; set; }

    public long? ChatStatus { get; set; }

    public decimal? LogOn { get; set; }

    public string? YoutubeChannel { get; set; }

    public virtual ICollection<Tbcontact> TbcontactContacts { get; set; } = new List<Tbcontact>();

    public virtual ICollection<Tbcontact> TbcontactMembers { get; set; } = new List<Tbcontact>();

    public virtual ICollection<Tbcontactrequest> TbcontactrequestFromMembers { get; set; } = new List<Tbcontactrequest>();

    public virtual ICollection<Tbcontactrequest> TbcontactrequestToMembers { get; set; } = new List<Tbcontactrequest>();

    public virtual ICollection<Tbmemberpostresponse> Tbmemberpostresponses { get; set; } = new List<Tbmemberpostresponse>();

    public virtual ICollection<Tbmemberpost> Tbmemberposts { get; set; } = new List<Tbmemberpost>();

    public virtual Tbmemberprofile? Tbmemberprofile { get; set; }

    public virtual Tbmemberprofilecontactinfo? Tbmemberprofilecontactinfo { get; set; }

    public virtual Tbmemberprofilepersonalinfo? Tbmemberprofilepersonalinfo { get; set; }

    public virtual ICollection<Tbmembersprivacysetting> Tbmembersprivacysettings { get; set; } = new List<Tbmembersprivacysetting>();

    public virtual ICollection<Tbmembersregistered> Tbmembersregistereds { get; set; } = new List<Tbmembersregistered>();

    public virtual ICollection<Tbmessage> TbmessageContacts { get; set; } = new List<Tbmessage>();

    public virtual ICollection<Tbmessage> TbmessageSenders { get; set; } = new List<Tbmessage>();

    public virtual ICollection<Tbnotificationsetting> Tbnotificationsettings { get; set; } = new List<Tbnotificationsetting>();
}
