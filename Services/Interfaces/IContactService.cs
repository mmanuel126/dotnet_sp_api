using dotnet_sp_api.Models.DTOs;

namespace dotnet_sp_api.Services.Interfaces
{
  public interface IContactService
  {
    List<MemberContacts> GetMemberContacts(int memberID);
    List<MemberContacts> GetMemberSuggestions(int memberID);
    void SendRequestContact(int memberID, int contactID);
    List<MemberContacts> SearchMemberContacts(int memberID, string searchText);
    List<MemberContacts> GetRequestsList(int memberID);
    List<MemberContacts> GetSearchContacts(int userID, string searchText);
    void AcceptRequest(int memberID, int contactID);
    void RejectRequest(int memberID, int contactID);
    void DeleteContact(int memberID, int contactID);
    List<Search> SearchResults(int memberID, string searchText);
    List<MemberContacts> GetWhosFollowingMe(int memberID);
    void UnfollowMember(int memberID, int contactID);
    void FollowMember(int memberID, int contactID);
    List<MemberContacts> GetPeopleIFollow(int memberID);
    string IsFollowingContact(int memberID, int contactID);
  }
}