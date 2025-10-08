namespace dotnet_sp_api.Models.DTOs
{
    /// <summary>
    /// Holds the system notifications to member info 
    /// </summary>
    public class NotificationsSetting
    {
        public int MemberID { get; set; }
        public int SendMsg { get; set; }
        public int AddAsFriend { get; set; }
        public int ConfirmFriendShipRequest { get; set; }
        public int RepliesToYourHelpQuest { get; set; }
    }

    /// <summary>
    /// Holds the password info for member. 
    /// </summary>
    public class PasswordData
    {
        public string Pwd { get; set; } = string.Empty;
        public string MemberID { get; set; } = string.Empty;
    }

    /// <summary>
    /// Holds the privacy search settings data.
    /// </summary>
    public class PrivacySearchSettings
    {
        public string ID { get; set; } = string.Empty;
        public string MemberID { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
        public string BasicInfo { get; set; } = string.Empty;
        public string PersonalInfo { get; set; } = string.Empty;
        public string PhotosTagOfYou { get; set; } = string.Empty;
        public string VideosTagOfYou { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string WorkInfo { get; set; } = string.Empty;
        public string IMdisplayName { get; set; } = string.Empty;
        public string MobilePhone { get; set; } = string.Empty;
        public string OtherPhone { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Visibility { get; set; } = string.Empty;
        public int ViewProfilePicture { get; set; }
        public int ViewFriendsList { get; set; }
        public int ViewLinksToRequestAddingYouAsFriend { get; set; }
        public int ViewLinkTSendYouMsg { get; set; }
        public string Email { get; set; } = string.Empty;
    }

    /// <summary>
    /// Holds member name info data - used by the Account controller.
    /// </summary>
    public class MemberNameInfo
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SecurityQuestion { get; set; } = string.Empty;
        public string SecurityAnswer { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
    }
}