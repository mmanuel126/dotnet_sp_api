using dotnet_sp_api.Models.DTOs;
using dotnet_sp_api.Helpers;
using dotnet_sp_api.Services.Interfaces;
using dotnet_sp_api.Repositories.Interfaces;

namespace dotnet_sp_api.Services.Implementations
{
    /// <summary>
    /// Settings service functionality code
    /// </summary>
    public class SettingService(ISettingRepository setRepo, IConfiguration configuration, Crypto crypto) : ISettingService
    {
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Gets the member name information.
        /// </summary>
        /// <returns>The member name info.</returns>
        /// <param name="memberID">Member identifier.</param>

        public List<MemberNameInfo> GetMemberNameInfo(int memberID)
        {
            return setRepo.GetMemberNameInfo(memberID);
        }

        /// <summary>
        /// Saves the member name info.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="fName">First name.</param>
        /// <param name="mName">Middle name.</param>
        /// <param name="lName">Last name.</param>
        public void SaveMemberNameInfo(int memberID, string fName, string mName, string lName)
        {
            setRepo.SaveMemberNameInfo(memberID, fName, mName, lName);
        }

        /// <summary>
        /// Saves the member email information.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="email">Email.</param>
        public void SaveMemberEmailInfo(int memberID, string email)
        {
            setRepo.SaveMemberEmailInfo(memberID, email);
        }

        /// <summary>
        /// Saves the password information.
        /// </summary>
        /// <param name="body"></param>
        public void SavePasswordInfo(PasswordData body)
        {
            body.Pwd = crypto.Encrypt(body.Pwd);
            setRepo.SavePasswordInfo(body.MemberID, body.Pwd);
        }

        /// <summary>
        /// Saves the security question info.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="questionID">Question identifier.</param>
        /// <param name="answer">Answer.</param>
        public void SaveSecurityQuestionInfo(int memberID, int questionID, string answer)
        {
            setRepo.SaveSecurityQuestionInfo(memberID, questionID, answer);
        }

        /// <summary>
        /// Deactivates the account.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="reason">Reason.</param>
        /// <param name="explanation">Explanation.</param>
        /// <param name="futureEmail">Future email.</param>
        public void DeactivateAccount(int memberID, int reason, string explanation, int? futureEmail)
        {
            setRepo.DeactivateAccount(memberID, reason, explanation, futureEmail);
        }

        /// <summary>
        /// Gets the member notifications.
        /// </summary>
        /// <returns>The member notifications.</returns>
        /// <param name="memberId">Member identifier.</param>
        public List<NotificationsSetting> GetMemberNotifications(int memberId)
        {
            return setRepo.GetMemberNotifications(memberId);
        }

        /// <summary>
        /// Saves the notification settings.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">Body.</param>
        public void SaveNotificationSettings(int memberID, NotificationsSetting body)
        {
            setRepo.SaveNotificationSettings(memberID, body.SendMsg, body.AddAsFriend, body.ConfirmFriendShipRequest,
                                           body.RepliesToYourHelpQuest);
        }

        /// <summary>
        /// Gets the profile settings.
        /// </summary>
        /// <returns>The profile settings.</returns>
        /// <param name="memberID">Member identifier.</param>
        public List<PrivacySearchSettings> GetProfileSettings(int memberID)
        {
            return setRepo.GetProfileSettings(memberID);
        }

        /// <summary>
        /// Saves the profile settings.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">Body.</param>
        public void SaveProfileSettings(int memberID, PrivacySearchSettings body)
        {
            setRepo.SaveProfileSettings(memberID, body);
        }

        /// <summary>
        /// Gets the privacy search settings.
        /// </summary>
        /// <returns>The privacy search settings.</returns>
        /// <param name="memberID">Member identifier.</param>
        public List<PrivacySearchSettings> GetPrivacySearchSettings(int memberID)
        {
            return setRepo.GetPrivacySearchSettings(memberID);
        }

        /// <summary>
        /// Saves the privacy search settings.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="visibility">Visibility.</param>
        /// <param name="viewProfilePicture">If set to <c>true</c> view profile picture.</param>
        /// <param name="viewFriendsList">If set to <c>true</c> view friends list.</param>
        /// <param name="viewLinkToRequestAddingYouAsFriend">If set to <c>true</c> view link to request adding you as friend.</param>
        /// <param name="viewLinkToSendYouMsg">If set to <c>true</c> view link to send you message.</param>
        public void SavePrivacySearchSettings(
               int memberID,
               int visibility,
               int viewProfilePicture,
               int viewFriendsList,
               int viewLinkToRequestAddingYouAsFriend,
               int viewLinkToSendYouMsg)
        {
            setRepo.SavePrivacySearchSettings(memberID, visibility, viewProfilePicture, viewFriendsList, viewLinkToRequestAddingYouAsFriend,
                                             viewLinkToSendYouMsg);
        }

        /// <summary>
        /// Updates profile picture file name.
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="fileName"></param>
        public void UpdateProfilePicture(int memberId, string fileName)
        {
            setRepo.UpdateProfilePicture(memberId, fileName);
        }
    }
}