using dotnet_sp_api.Models.DTOs;

namespace dotnet_sp_api.Services.Interfaces
{
    public interface ISettingService
    {
        List<MemberNameInfo> GetMemberNameInfo(int memberID);
        void SaveMemberNameInfo(int memberID, string fName, string mName, string lName);
        void SaveMemberEmailInfo(int memberID, string email);
        void SavePasswordInfo(PasswordData body);
        public void SaveSecurityQuestionInfo(int memberID, int questionID, string answer);
        void DeactivateAccount(int memberID, int reason, string explanation, int? futureEmail);
        List<NotificationsSetting> GetMemberNotifications(int memberId);
        void SaveNotificationSettings(int memberID, NotificationsSetting body);
        List<PrivacySearchSettings> GetProfileSettings(int memberID);
        void SaveProfileSettings(int memberID, PrivacySearchSettings body);
        List<PrivacySearchSettings> GetPrivacySearchSettings(int memberID);
        void SavePrivacySearchSettings(
             int memberID,
              int visibility,
              int viewProfilePicture,
              int viewFriendsList,
              int viewLinkToRequestAddingYouAsFriend,
              int viewLinkToSendYouMsg);

        void UpdateProfilePicture(int memberId, string fileName);
    }
}