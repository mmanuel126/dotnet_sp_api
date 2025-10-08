using dotnet_sp_api.Models.DBContextModels;
using dotnet_sp_api.Models.DTOs;

namespace dotnet_sp_api.Repositories.Interfaces
{
  public interface ISettingRepository
  {
    List<MemberNameInfo> GetMemberNameInfo(int memberID);
    void SaveMemberNameInfo(int memberID, string fName, string mName, string lName);
    void SaveMemberEmailInfo(int memberID, string email);
    void SavePasswordInfo(string memberID, string pwd);
    void SaveSecurityQuestionInfo(int memberID, int questionID, string answer);
    void DeactivateAccount(int memberID, int reason, string explanation, int? futureEmail);
    bool ValidateMemberId(int memberID);
    List<NotificationsSetting> GetMemberNotifications(int memberId);
    public void SaveNotificationSettings(
          int MemberID,
          int LG_SendMsg,
          int LG_AddAsFriend,
          int LG_ConfirmFriendShipRequest,
          int HE_RepliesToYourHelpQuest
    );
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
    List<Tbmemberprofile> GetAllMembers(string searchText);
    bool IsEmailExist(int memberID, string email);
    List<Tbmemberprofilepicture> GetMemberProfilePictures(int memberID);
    string GetMemberDefaultPicture(int memberID);
    void RemoveProfilePicture(int memberID, string noPhotoFilename);
    void SetPictureAsDefault(int memberID, int profileID, string fileName);
    void RemovePicture(int profileID, string defaultFileName);

    void UpdateProfilePicture(int memberId, string fileName);
  }
}