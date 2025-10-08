
namespace dotnet_sp_api.Models.DTOs
{
    /// <summary>
    /// Stores member profile basic info
    /// </summary>
    public class MemberProfileBasicInfo
    {
        public string MemProfileImage { get; set; } = string.Empty;
        public string MemProfileName { get; set; } = string.Empty;
        public string MemberProfileTitle { get; set; } = string.Empty;
        public string MemProfileBusinessSector { get; set; } = string.Empty;
        public string MemProfileIndustry { get; set; } = string.Empty;
        public string MemProfileStatus { get; set; } = string.Empty;
        public string MemProfileGender { get; set; } = string.Empty;
        public string MemProfileDOB { get; set; } = string.Empty;
        public string MemProfileInterestedInc { get; set; } = string.Empty;
        public string MemProfileLookingFor { get; set; } = string.Empty;
        public string CurrentCity { get; set; } = string.Empty;
        public string CurrentStatus { get; set; } = string.Empty;
        public string DOBDay { get; set; } = string.Empty;
        public string DOBMonth { get; set; } = string.Empty;
        public string DOBYear { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string HomeNeighborhood { get; set; } = string.Empty;
        public string Hometown { get; set; } = string.Empty;
        public string InterestedInType { get; set; } = string.Empty;
        public string JoinedDate { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LookingForEmployment { get; set; } = string.Empty;
        public string LookingForNetworking { get; set; } = string.Empty;
        public string LookingForPartnership { get; set; } = string.Empty;
        public string LookingForRecruitment { get; set; } = string.Empty;
        public string MemberID { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string PoliticalView { get; set; } = string.Empty;
        public string ReligiousView { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string ShowDOBType { get; set; } = string.Empty;
        public string ShowSexInProfile { get; set; } = string.Empty;
        public string GetLGEntitiesCount { get; set; } = string.Empty;
    }

    /// <summary>
    /// Stores Member profile contact info  
    /// </summary>
    public class MemberContactInfo
    {
        public string Email { get; set; } = string.Empty;
        public string OtherEmail { get; set; } = string.Empty;
        public string Facebook { get; set; } = string.Empty;
        public string Instagram { get; set; } = string.Empty;
        public string Twitter { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string HomePhone { get; set; } = string.Empty;
        public string CellPhone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public int ShowAddress { get; set; }
        public int ShowEmailToMembers { get; set; }
        public int ShowCellPhone { get; set; }
        public int ShowHomePhone { get; set; }
    }

    /// <summary>
    /// Stores member profile education info  
    /// </summary>
    public class MemberProfileEducation
    {
        public String SchoolID { get; set; } = string.Empty;
        public String SchoolName { get; set; } = string.Empty;
        public String SchoolImage { get; set; } = string.Empty;
        public String SchoolAddress { get; set; } = string.Empty;
        public String Major { get; set; } = string.Empty;
        public String Degree { get; set; } = string.Empty;
        public String DegreeTypeID { get; set; } = string.Empty;
        public String YearClass { get; set; } = string.Empty;
        public String SchoolType { get; set; } = string.Empty;
        public String Societies { get; set; } = string.Empty;
        public String SportLevelType { get; set; } = string.Empty;

    }

    /// <summary>
    /// Holds the recent post child info.
    /// </summary>
    public class PostResponse
    {
        public int? PostResponseID { get; set; }
        public int? PostID { get; set; }
        public string Description { get; set; } = string.Empty;
        public string DateResponded { get; set; } = string.Empty;
        public int? MemberID { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
    }

    /// <summary>
    /// Holds the member profile general info -  - Used by the Member controller.
    /// </summary>
    public class MemberProfileGenInfo
    {
        public string MemberID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public bool ShowSexInProfile { get; set; } = false;
        public string DOBMonth { get; set; } = string.Empty;
        public string DOBDay { get; set; } = string.Empty;
        public string DOBYear { get; set; } = string.Empty;
        public bool ShowDOBType { get; set; } = false;
        public string Hometown { get; set; } = string.Empty;
        public string HomeNeighborhood { get; set; } = string.Empty;
        public string CurrentStatus { get; set; } = string.Empty;
        public string InterestedInType { get; set; } = string.Empty;
        public bool LookingForEmployment { get; set; } = false;
        public bool LookingForRecruitment { get; set; } = false;
        public bool LookingForPartnership { get; set; } = false;
        public bool LookingForNetworking { get; set; } = false;
        public string PicturePath { get; set; } = string.Empty;
        public string JoinedDate { get; set; } = string.Empty;
        public string CurrentCity { get; set; } = string.Empty;
        public string TitleDesc { get; set; } = string.Empty;
        public string Sport { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string LeftRightHandFoot { get; set; } = string.Empty;
        public string PreferredPosition { get; set; } = string.Empty;
        public string SecondaryPosition { get; set; } = string.Empty;
        public string InterestedDesc { get; set; } = string.Empty;
    }

    /// <summary>
    /// Holds member posts info  - Used by the Member controller.
    /// </summary>
    public class Posts
    {
        public string PostID { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DatePosted { get; set; } = string.Empty;
        public string AttachFile { get; set; } = string.Empty;
        public string MemberID { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string ChildPostCnt { get; set; } = string.Empty;
    }

    /// <summary>
    /// Holds youtube channel data obtained from youtube  - Used by the Member controller.
    /// </summary>
    public class YoutubeChannel
    {
        public string MemberID { get; set; } = string.Empty;
        public string ChannelID { get; set; } = string.Empty;
    }

    /// <summary>
    /// Holds instagram info obtained from instagram site  - Used by the Member controller.
    /// </summary>
    public class InstagramURL
    {
        public string MemberID { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }

    /// <summary>
    /// stores the youtube playlist info obtained from youtube API - Used by the Member controller.
    /// </summary>
    public class YoutubePlayList
    {
        public string Etag { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DefaultThumbnail { get; set; } = string.Empty;
        public string DefaultThumbnailHeight { get; set; } = string.Empty;
        public string DefaultThumbnailWidth { get; set; } = string.Empty;
    }

    /// <summary>
    /// Stores utube videos list from Google API 
    /// </summary>
    public class YoutubeVideosList
    {
        public string Etag { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DefaultThumbnail { get; set; } = string.Empty;
        public string DefaultThumbnailHeight { get; set; } = string.Empty;
        public string DefaultThumbnailWidth { get; set; } = string.Empty;
        public string PublishedAt { get; set; } = string.Empty;
    }

}