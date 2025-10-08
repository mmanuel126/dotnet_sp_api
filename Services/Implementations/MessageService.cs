using dotnet_sp_api.Models.DTOs;
using dotnet_sp_api.Repositories.Interfaces;
using dotnet_sp_api.Services.Interfaces;

namespace dotnet_sp_api.Services.Implementations
{
    /// <summary>
    /// Messages service functionality code
    /// </summary>
    public class MessageService(IMessageRepository _msgRepo) : IMessageService
    {

        /// <summary>
        /// Gets the member messages.
        /// </summary>
        /// <returns>The member messages.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="showType">Show type.</param>
        public List<SearchMessages> GetMemberMessages(int memberID, string showType)
        {
            var mlist = _msgRepo.GetMemberMessages(memberID, showType);
            return mlist;
        }

        /// <summary>
        /// Gets the total unread messages.
        /// </summary>
        /// <returns>The total unread messages.</returns>
        /// <param name="memberID">Member identifier.</param>
        public int GetTotalUnreadMessages(int memberID)
        {
            int res = _msgRepo.GetTotalUnreadMessages(memberID);
            return (res);
        }

        /// <summary>
        /// Creates a message.
        /// </summary>
        /// <param name="body">Body</param>
        public void CreateMessage(MessageInputs body)
        {
            _msgRepo.CreateMessage(body);
        }



        /// <summary>
        /// Toggles the state of the message.
        /// </summary>
        /// <param name="status">Status.</param>
        /// <param name="msgID">Message identifier.</param>
        public void ToggleMessageState(int status, int msgID)
        {
            _msgRepo.ToggleMessageState(status, msgID);
        }

        /// <summary>
        /// Gets the message info by identifier.
        /// </summary>
        /// <returns>The message info by identifier.</returns>
        /// <param name="msgID">Message identifier.</param>
        public List<MessageInfo> GetMessageInfoByID(int msgID)
        {
            List<MessageInfo> lst = _msgRepo.GetMessageInfoByID(msgID);
            return lst;
        }

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="msgID">Message identifier.</param>
        public void DeleteMessage(int msgID)
        {
            _msgRepo.DeleteMessage(msgID);
        }

        /// <summary>
        /// Searchs messages given search key for member id.
        /// </summary>
        /// <returns>The messages.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchKey">Search key.</param>
        public List<SearchMessages> SearchMessages(int memberID, string searchKey)
        {
            List<SearchMessages> msgList = _msgRepo.SearchMessages(memberID, searchKey);
            return msgList;
        }
    }
}