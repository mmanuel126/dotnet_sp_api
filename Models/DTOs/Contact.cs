using Microsoft.AspNetCore.Authentication.BearerToken;

namespace dotnet_sp_api.Models.DTOs
{
    /// <summary>
    /// Stores contacts searched info 
    /// </summary>
    public class ContactSearch
    {
        public string EntityID { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CityState { get; set; } = string.Empty;
        public string LabelText { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NameAndID { get; set; } = string.Empty;
        public string Params { get; set; } = string.Empty;
        public string ParamsAV { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MemberCount { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }

    /// <summary>
    /// Stores entity (any data from the site, i.e., contact, etc) 
    /// </summary>
    public class Entity
    {
        public string EntityID { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CityState { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    /// <summary>
    /// Stores members info by type - Used by the Contact controller.
    /// </summary>
    public class MemberByType
    {
        public string MemberID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string TypeVal { get; set; } = string.Empty;
        public string IsFriend { get; set; } = string.Empty;
        public string IsSamePerson { get; set; } = string.Empty;
    }

    /// <summary>
    /// Stores member contacts info 
    /// </summary>
    public class MemberContacts
    {
        public string FriendName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string ContactID { get; set; } = string.Empty;
        public string ShowType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string TitleDesc { get; set; } = string.Empty;
        public string Params { get; set; } = string.Empty;
        public string ParamsAV { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string LabelText { get; set; } = string.Empty;
        public string NameAndID { get; set; } = string.Empty;
        public string ShowFollow { get; set; } = string.Empty;
    }

    /// <summary>
    /// Stores the memberid return by the result query  
    /// </summary>
    public class Result
    {
        public int MemberID { get; set; } = 0;
    }

    /// <summary>
    /// Stores the searched info.
    /// </summary>
    public class Search
    {
        public int EntityID { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string EventDate { get; set; } = string.Empty;
        public string Rsvp { get; set; } = string.Empty;
        public string Params { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MemberCount { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CityState { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NameAndID { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LabelText { get; set; } = string.Empty;
        public string ShowCancel { get; set; } = string.Empty;
        public string ParamsAV { get; set; } = string.Empty;
        public string Stype { get; set; } = string.Empty;
    }


}