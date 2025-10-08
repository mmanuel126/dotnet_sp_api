using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbmembersprivacysetting
{
    public long Id { get; set; }

    public long MemberId { get; set; }

    public long? Profile { get; set; }

    public long? BasicInfo { get; set; }

    public long? PersonalInfo { get; set; }

    public long? PhotosTagOfYou { get; set; }

    public long? VideosTagOfYou { get; set; }

    public long? ContactInfo { get; set; }

    public long? Education { get; set; }

    public long? WorkInfo { get; set; }

    public long? ImDisplayName { get; set; }

    public long? MobilePhone { get; set; }

    public long? OtherPhone { get; set; }

    public long? EmailAddress { get; set; }

    public long? Visibility { get; set; }

    public decimal? ViewProfilePicture { get; set; }

    public decimal? ViewFriendsList { get; set; }

    public decimal? ViewLinkToRequestAddingYouAsFriend { get; set; }

    public decimal? ViewLinkToSendYouMsg { get; set; }

    public virtual Tbmember Member { get; set; } = null!;
}
