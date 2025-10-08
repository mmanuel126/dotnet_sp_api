using dotnet_sp_api.Models.DTOs;

namespace dotnet_sp_api.Services.Interfaces
{
    public interface IMessageService
    {
        List<SearchMessages> GetMemberMessages(int memberID, string showType);
        int GetTotalUnreadMessages(int memberID);
        void CreateMessage(MessageInputs body);
        void ToggleMessageState(int status, int msgID);
        List<MessageInfo> GetMessageInfoByID(int msgID);
        void DeleteMessage(int msgID);
        List<SearchMessages> SearchMessages(int memberID, string searchKey);
    }
}
