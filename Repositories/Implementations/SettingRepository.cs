
using dotnet_sp_api.Models.DBContextModels;
using dotnet_sp_api.Repositories.Interfaces;
using dotnet_sp_api.Models.DTOs;

namespace dotnet_sp_api.Repositories.Implementations
{
    /// <summary>
    /// Describes the functionalities for accessing data for Member settings
    /// </summary>
    public class SettingRepository(DBContext context) : ISettingRepository
    {
        /// <summary>
        /// Get member id's name information
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<MemberNameInfo> GetMemberNameInfo(int memberID)
        {
            var lst = (from m in context.Tbmembers
                       join pp in context.Tbmemberprofiles on m.MemberId equals pp.MemberId
                       where m.MemberId == memberID

                       select new MemberNameInfo()
                       {
                           FirstName = pp.FirstName ?? "",
                           LastName = pp.LastName ?? "",
                           MiddleName = pp.MiddleName ?? "",
                           Email = m.Email ?? "",
                           SecurityQuestion = m.SecurityQuestion.ToString() ?? "",
                           SecurityAnswer = m.SecurityAnswer ?? "",
                           PassWord = m.Password ?? ""
                       }).ToList();

            return lst;
        }

        /// <summary>
        /// Save member ID's name information
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="fName"></param>
        /// <param name="mName"></param>
        /// <param name="lName"></param>
        public void SaveMemberNameInfo(int memberID, string fName, string mName, string lName)
        {
            var p = (from m in context.Tbmemberprofiles where m.MemberId == memberID select m).First();
            p.LastName = lName;
            p.FirstName = fName;
            p.MiddleName = mName;
            context.SaveChanges();
        }

        /// <summary>
        /// Saves member ID email information 
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="email"></param>
        public void SaveMemberEmailInfo(int memberID, string email)
        {
            List<Tbmember> p = (from m in context.Tbmembers where m.Email == email select m).ToList();
            if (p.Count == 0)
            {
                var q = (from m in context.Tbmembers where m.MemberId == memberID select m).First();
                q.Email = email;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Save member iDs save password information
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="pwd"></param>
        public void SavePasswordInfo(string memberID, string pwd)
        {
            var q = (from m in context.Tbmembers where m.MemberId == Convert.ToInt32(memberID) select m).First();
            q.Password = pwd;
            context.SaveChanges();
        }

        /// <summary>
        /// Saves member IDs security question information
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="questionID"></param>
        /// <param name="answer"></param>
        public void SaveSecurityQuestionInfo(int memberID, int questionID, string answer)
        {
            var q = (from m in context.Tbmembers where m.MemberId == memberID select m).First();
            q.SecurityQuestion = questionID;
            q.SecurityAnswer = answer;
            context.SaveChanges();
        }

        /// <summary>
        /// Deactivate account for member ID.
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="reason"></param>
        /// <param name="explanation"></param>
        /// <param name="futureEmail"></param>
        public void DeactivateAccount(int memberID, int reason, string explanation, int? futureEmail)
        {
            var q = (from m in context.Tbmembers where m.MemberId == memberID select m).First();
            q.Status = 3;
            q.DeactivateReason = reason;
            q.DeactivateExplanation = explanation;
            q.FutureEmails = futureEmail;
            context.SaveChanges();
        }

        /// <summary>
        /// Validates member ID exist
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public bool ValidateMemberId(int memberID)
        {
            var q = (from m in context.Tbmembers where m.MemberId == memberID select m).ToList();
            return (q.Count != 0);
        }

        public List<NotificationsSetting> GetMemberNotifications(int memberId)
        {
            var q = (from m in context.Tbnotificationsettings
                     where m.MemberId == memberId
                     select new NotificationsSetting()
                     {
                         MemberID = (int)m.MemberId,
                         SendMsg = (int)m.SendMsg!,
                         AddAsFriend = (int)m.AddAsFriend!,
                         ConfirmFriendShipRequest = (int)m.ConfirmFriendshipRequest!,
                         RepliesToYourHelpQuest = (int)m.RepliesToYourHelpQuest!
                     }).ToList();
            return q;
        }

        public void SaveNotificationSettings(
                  int MemberID,
                  int SendMsg,
                  int AddAsFriend,
                  int ConfirmFriendShipRequest,
                  int RepliesToYourHelpQuest
            )
        {
            var n = (from m in context.Tbnotificationsettings where m.MemberId == MemberID select m).ToList();

            if (n.Count != 0)
            {
                var q = n.First();
                q.SendMsg = (int)SendMsg;
                q.AddAsFriend = (int)AddAsFriend;
                q.ConfirmFriendshipRequest = ConfirmFriendShipRequest;
                q.RepliesToYourHelpQuest = RepliesToYourHelpQuest;
                context.SaveChanges();
            }
            else
            {
                Tbnotificationsetting ps = new Tbnotificationsetting();
                ps.MemberId = MemberID;
                ps.SendMsg = SendMsg;
                ps.AddAsFriend = AddAsFriend;
                ps.ConfirmFriendshipRequest = ConfirmFriendShipRequest;
                ps.RepliesToYourHelpQuest = RepliesToYourHelpQuest;
                context.Tbnotificationsettings.Add(ps);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Get profile privacy settings
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<PrivacySearchSettings> GetProfileSettings(int memberID)
        {
            List<PrivacySearchSettings> lst = (from p in context.Tbmembersprivacysettings
                                               join m in context.Tbmembers on p.MemberId equals m.MemberId
                                               where p.MemberId == memberID
                                               select new PrivacySearchSettings()
                                               {
                                                   ID = p.Id.ToString(),
                                                   MemberID = p.MemberId.ToString(),
                                                   Profile = p.Profile.ToString() ?? "",
                                                   BasicInfo = p.BasicInfo.ToString() ?? "",
                                                   PersonalInfo = p.PersonalInfo.ToString() ?? "",
                                                   PhotosTagOfYou = p.PhotosTagOfYou.ToString() ?? "",
                                                   VideosTagOfYou = p.VideosTagOfYou.ToString() ?? "",
                                                   ContactInfo = p.ContactInfo.ToString() ?? "",
                                                   Education = p.Education.ToString() ?? "",
                                                   WorkInfo = p.WorkInfo.ToString() ?? "",
                                                   IMdisplayName = p.ImDisplayName.ToString() ?? "",
                                                   MobilePhone = p.MobilePhone.ToString() ?? "",
                                                   OtherPhone = p.OtherPhone.ToString() ?? "",
                                                   EmailAddress = p.EmailAddress.ToString() ?? "",
                                                   Visibility = p.Visibility.ToString() ?? "",
                                                   ViewProfilePicture = (int)p.ViewProfilePicture!,
                                                   ViewFriendsList = (int)p.ViewFriendsList!,
                                                   ViewLinksToRequestAddingYouAsFriend = (int)p.ViewLinkToRequestAddingYouAsFriend!,
                                                   ViewLinkTSendYouMsg = (int)p.ViewLinkToSendYouMsg!,
                                                   Email = m.Email!.ToString()
                                               }).ToList();
            return lst;
        }

        /// <summary>
        /// Saves the profile settings.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">Body.</param>
        public void SaveProfileSettings(
                  int memberID, PrivacySearchSettings body)
        {
            var p = (from m in context.Tbmembersprivacysettings where m.MemberId == memberID select m).First();
            if (p != null)
            {
                p.MemberId = memberID;
                p.Profile = int.Parse(body.Profile);
                p.BasicInfo = int.Parse(body.BasicInfo);
                p.PersonalInfo = int.Parse(body.PersonalInfo);
                p.PhotosTagOfYou = int.Parse(body.PhotosTagOfYou);
                p.VideosTagOfYou = int.Parse(body.VideosTagOfYou);
                p.ContactInfo = int.Parse(body.ContactInfo);
                p.Education = int.Parse(body.Education);
                p.WorkInfo = int.Parse(body.WorkInfo);
                p.ImDisplayName = int.Parse(body.IMdisplayName);
                p.MobilePhone = int.Parse(body.MobilePhone);
                p.OtherPhone = int.Parse(body.OtherPhone);
                p.EmailAddress = int.Parse(body.EmailAddress);
                context.SaveChanges();
            }
            else
            {
                Tbmembersprivacysetting ps = new Tbmembersprivacysetting();
                ps.MemberId = memberID;
                ps.Profile = int.Parse(body.Profile);
                ps.BasicInfo = int.Parse(body.BasicInfo);
                ps.PersonalInfo = int.Parse(body.PersonalInfo);
                ps.PhotosTagOfYou = int.Parse(body.PhotosTagOfYou);
                ps.VideosTagOfYou = int.Parse(body.VideosTagOfYou);
                ps.ContactInfo = int.Parse(body.ContactInfo);
                ps.Education = int.Parse(body.Education);
                ps.WorkInfo = int.Parse(body.WorkInfo);
                ps.ImDisplayName = int.Parse(body.IMdisplayName);
                ps.MobilePhone = int.Parse(body.MobilePhone);
                ps.OtherPhone = int.Parse(body.OtherPhone);
                ps.EmailAddress = int.Parse(body.EmailAddress);
                context.Tbmembersprivacysettings.Add(ps);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Get privacy search settings
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<PrivacySearchSettings> GetPrivacySearchSettings(int memberID)
        {
            return (GetProfileSettings(memberID));
        }

        /// <summary>
        /// saves privacy search settings
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="visibility"></param>
        /// <param name="viewProfilePicture"></param>
        /// <param name="viewFriendsList"></param>
        /// <param name="viewLinkToRequestAddingYouAsFriend"></param>
        /// <param name="viewLinkToSendYouMsg"></param>
        public void SavePrivacySearchSettings(
              int memberID,
              int visibility,
              int viewProfilePicture,
              int viewFriendsList,
              int viewLinkToRequestAddingYouAsFriend,
              int viewLinkToSendYouMsg)
        {
            var p = (from m in context.Tbmembersprivacysettings where m.MemberId == memberID select m).First();
            if (p != null)
            {
                p.MemberId = memberID;
                p.Visibility = visibility;
                p.ViewProfilePicture = (int)viewProfilePicture;
                p.ViewFriendsList = (int)viewFriendsList;
                p.ViewLinkToRequestAddingYouAsFriend = (int)viewLinkToRequestAddingYouAsFriend;
                p.ViewLinkToSendYouMsg = (int)viewLinkToSendYouMsg;
                context.SaveChanges();
            }
            else
            {
                Tbmembersprivacysetting ps = new Tbmembersprivacysetting();
                ps.MemberId = memberID;
                ps.Visibility = visibility;
                ps.ViewFriendsList = viewFriendsList;
                ps.ViewLinkToRequestAddingYouAsFriend = viewLinkToRequestAddingYouAsFriend;
                ps.ViewLinkToSendYouMsg = viewLinkToSendYouMsg;
                context.Tbmembersprivacysettings.Add(ps);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Get all members for search text
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<Tbmemberprofile> GetAllMembers(string searchText)
        {
            var lst = (from m in context.Tbmemberprofiles where (m.FirstName!.Contains(searchText)) || (m.LastName!.Contains(searchText)) select m);
            return lst.ToList();
        }

        /// <summary>
        /// return true if email exist for member id
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmailExist(int memberID, string email)
        {
            var q = (from m in context.Tbmembers where m.Email == email && m.MemberId != memberID select m).ToList();
            if (q.Count != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// returns a list of members profile pictures.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<Tbmemberprofilepicture> GetMemberProfilePictures(int memberID)
        {
            var q = (from m in context.Tbmemberprofilepictures where m.MemberId == memberID && (m.Removed == 0 || m.Removed == null) select m).ToList();
            return (q.ToList());
        }

        /// <summary>
        /// returns members default profile picture.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public string GetMemberDefaultPicture(int memberID)
        {
            var q = (from m in context.Tbmemberprofilepictures where m.MemberId == memberID && m.IsDefault == 1 select m).ToList();
            if (q.Count != 0)
            {
                return (q[0].FileName!.ToString());
            }
            else
                return "";
        }

        /// <summary>
        /// remove profile picture from list
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="noPhotoFilename"></param>
        public void RemoveProfilePicture(int memberID, string noPhotoFilename)
        {
            //update tb meber profile with new profile picture
            var mbr = (from m in context.Tbmemberprofiles where m.MemberId == memberID select m).First();
            mbr.PicturePath = noPhotoFilename;
            context.SaveChanges();

            //remove the old default picture for the member 
            var mq = (from q in context.Tbmemberprofilepictures where q.MemberId == memberID && q.IsDefault == 1 select q);
            if (mq.Count() != 0)
            {
                var f = mq.First();
                f.IsDefault = 0;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Set picture as default
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="profileID"></param>
        /// <param name="fileName"></param>
        public void SetPictureAsDefault(int memberID, int profileID, string fileName)
        {
            //remove the old default picture for the member 
            var mq = (from q in context.Tbmemberprofilepictures where q.MemberId == memberID && q.IsDefault == 1 select q);
            if (mq.Count() != 0)
            {
                var f = mq.First();
                f.IsDefault = 0;
                context.SaveChanges();
            }
            //set the new default picture 
            var newq = (from q in context.Tbmemberprofilepictures where q.MemberId == memberID && q.ProfileId == profileID select q);
            if (newq.Count() != 0)
            {
                var n = newq.First();
                n.IsDefault = 1;
                context.SaveChanges();
            }

            //update tb meber profile with new profile picture
            var mbr = (from m in context.Tbmemberprofiles where m.MemberId == memberID select m).First();
            mbr.PicturePath = fileName;
            context.SaveChanges();
        }

        /// <summary>
        /// Remove picture as profile
        /// </summary>
        /// <param name="profileID"></param>
        /// <param name="defaultFileName"></param>
        public void RemovePicture(int profileID, string defaultFileName)
        {
            //get profile id to delete and then delete it.
            var newq = (from q in context.Tbmemberprofilepictures where q.ProfileId == profileID select q);
            if (newq.Count() != 0)
            {
                bool isDef = false;
                int memberID = 0;

                var n = newq.First();
                memberID = (int)n.MemberId;
                if (n.IsDefault == 1) isDef = true;

                n.Removed = 1;
                n.IsDefault = 0;
                context.SaveChanges();

                //update tbmeber profile with new profile picture
                if (isDef)
                {
                    var mbr = (from m in context.Tbmemberprofiles where m.MemberId == memberID select m).First();
                    mbr.PicturePath = defaultFileName;
                    context.SaveChanges();
                }
            }
        }

        public void UpdateProfilePicture(int memberId, string fileName)
        {
            //update tb meber profile with new profile picture
            var mbr = (from m in context.Tbmemberprofiles where m.MemberId == memberId select m).First();
            mbr.PicturePath = fileName;
            context.SaveChanges();
        }
    }
}
