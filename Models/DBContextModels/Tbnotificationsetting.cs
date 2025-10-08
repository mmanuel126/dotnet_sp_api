using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbnotificationsetting
{
    public long Id { get; set; }

    public long MemberId { get; set; }

    public decimal? SendMsg { get; set; }

    public decimal? AddAsFriend { get; set; }

    public decimal? ConfirmFriendshipRequest { get; set; }

    public decimal? Poking { get; set; }

    public decimal? ConfirmFriendDetails { get; set; }

    public decimal? RequestToListAsFamily { get; set; }

    public decimal? AddsFriendYouSuggest { get; set; }

    public decimal? HasBirthdayComingup { get; set; }

    public decimal? TagInPhoto { get; set; }

    public decimal? TagOneOfYourPhotos { get; set; }

    public decimal? TagsInVideo { get; set; }

    public decimal? TagsOneOfYourVideos { get; set; }

    public decimal? CommentOnVideo { get; set; }

    public decimal? CommentOnVideoOfYou { get; set; }

    public decimal? CommentAfterYouInVideo { get; set; }

    public decimal? RepliesToYourHelpQuest { get; set; }

    public virtual Tbmember Member { get; set; } = null!;
}
