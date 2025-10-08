using dotnet_sp_api.Repositories.Interfaces;
using dotnet_sp_api.Services.Interfaces;
using dotnet_sp_api.Models.DTOs;

namespace dotnet_sp_api.Services.Implementations
{
    /// <summary>
    /// Contact service functionality code
    /// </summary>
    public class ContactService(IContactRepository contactRepository) : IContactService
    {
        readonly IContactRepository _contactRepo = contactRepository;

        /// <summary>
        /// Gets a list of member contacts by the given member ID and show type.
        /// </summary>
        /// <returns>The member contacts.</returns>
        /// <param name="memberID">Member identifier.</param>
        public List<MemberContacts> GetMemberContacts(int memberID)
        {
            List<MemberContacts> lst = _contactRepo.GetMemberContacts(memberID).ToList();
            return lst;
        }

        /// <summary>
        /// Gets a list of member contact suggestoins by the given member ID and show type.
        /// </summary>
        /// <returns>The member contacts.</returns>
        /// <param name="memberID">Member identifier.</param>
        public List<MemberContacts> GetMemberSuggestions(int memberID)
        {
            List<MemberContacts> lst = _contactRepo.GetMemberSuggestions(memberID).ToList();
            return lst;
        }

        /// <summary>
        /// Sends the request to contact.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        public void SendRequestContact(int memberID, int contactID)
        {
            _contactRepo.SendRequestContact(memberID, contactID);
        }

        /// <summary>
        /// Searchs and return list of contacts for a given member ID and search Text.
        /// </summary>
        /// <returns>The member contacts.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchText">Search text.</param>
        public List<MemberContacts> SearchMemberContacts(int memberID, string searchText)
        {
            List<MemberContacts> lst = _contactRepo.SearchMemberContacts(memberID, searchText);
            return lst;
        }

        /// <summary>
        /// Gets the requests list.
        /// </summary>
        /// <returns>The requests list.</returns>
        /// <param name="memberID">Member identifier.</param>
        public List<MemberContacts> GetRequestsList(int memberID)
        {
            List<MemberContacts> lst = _contactRepo.GetRequestsList(memberID).ToList();
            return lst;
        }

        /// <summary>
        /// Gets the search contacts.
        /// </summary>
        /// <returns>The search contacts.</returns>          
        /// <param name="userID">User identifier.</param>
        /// <param name="searchText">Search text.</param>
        public List<MemberContacts> GetSearchContacts(int userID, string searchText)
        {
            List<MemberContacts> lst = _contactRepo.GetSearchContacts(userID, searchText).ToList();
            return lst;
        }

        /// <summary>
        /// member accepts request from contact 
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        public void AcceptRequest(int memberID, int contactID)
        {
            _contactRepo.AcceptRequest(memberID, contactID);
        }

        /// <summary>
        /// Member rejects the request from contact.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        public void RejectRequest(int memberID, int contactID)
        {
            _contactRepo.RejectRequest(memberID, contactID);
        }

        /// <summary>
        /// Deletes the contact.
        /// </summary>  
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        public void DeleteContact(int memberID, int contactID)
        {
            _contactRepo.DeleteContact(memberID, contactID);
        }

        /// <summary>
        /// reuturns the list of Contacts by search text.
        /// </summary>
        /// <returns>The results.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchText">Search text.</param>
        public List<Search> SearchResults(int memberID, string searchText)
        {
            List<Search> lst = _contactRepo.SearchResults(memberID, searchText).ToList();
            return lst;
        }

        /// <summary>
        /// reuturns the list of members I am following
        /// </summary>
        /// <returns>The results.</returns>
        /// <param name="memberID">Member identifier.</param>
        public List<MemberContacts> GetPeopleIFollow(int memberID)
        {
            List<MemberContacts> lst = _contactRepo.GetPeopleIFollow(memberID).ToList();
            return lst;
        }

        /// <summary>
        /// reuturns the list of whose following me.
        /// </summary>
        /// <returns>The results.</returns>
        /// <param name="memberID">Member identifier.</param>
        public List<MemberContacts> GetWhosFollowingMe(int memberID)
        {
            List<MemberContacts> lst = _contactRepo.GetWhosFollowingMe(memberID).ToList();
            return lst;
        }

        /// <summary>
        /// follow member
        /// </summary>  
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        public void FollowMember(int memberID, int contactID)
        {
            _contactRepo.FollowMember(memberID, contactID);
        }

        /// <summary>
        /// Unfollow member
        /// </summary>  
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        public void UnfollowMember(int memberID, int contactID)
        {
            _contactRepo.UnfollowMember(memberID, contactID);
        }
    }
}