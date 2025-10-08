using dotnet_sp_api.Services.Interfaces;
using dotnet_sp_api.Models.DTOs;

namespace dotnet_sp_api.Services.Implementations
{
    /// <summary>
    /// Member service functionality code
    /// </summary>
    public class MemberService(IMemberRepository memRepo) : IMemberService
    {
        public void CreateMemberPost(int memberID, string postMsg)
        {
            memRepo.CreateMemberPost(memberID, postMsg);
        }

        public void CreateMemberPostResponse(int memberID, int postID, string postMsg)
        {
            memRepo.CreateMemberPostResponse(memberID, postID, postMsg);
        }

        public List<PostResponse> GetMemberPostResponses(int postID)
        {
            return memRepo.GetMemberPostResponses(postID);
        }

        public List<Posts> GetMemberPosts(int memberID)
        {
            return memRepo.GetMemberPosts(memberID);
        }

        public MemberProfileGenInfo GetMemberGeneralInfo(int memberID)
        {
            return memRepo.GetMemberGeneralInfo(memberID);
        }

        public MemberContactInfo GetMemberContactInfo(int memberID)
        {
            return memRepo.GetMemberContactInfo(memberID);
        }

        public List<MemberProfileEducation> GetMemberEducationInfo(int memberID)
        {
            return memRepo.GetMemberEducationInfo(memberID);
        }

        public void SaveMemberGeneralInfo(MemberProfileGenInfo body)
        {
            memRepo.SaveMemberGeneralInfo(body);
        }

        public void SaveMemberContactInfo(int memberID, MemberContactInfo body)
        {
            memRepo.SaveMemberContactInfo(memberID, body);
        }

        public void AddMemberSchool(int memberID, MemberProfileEducation body)
        {
            memRepo.AddMemberSchool(memberID, body);
        }

        public void UpdateMemberSchool(int memberID, MemberProfileEducation body)
        {
            memRepo.UpdateMemberSchool(memberID, body);
        }
        public void RemoveMemberSchool(int memberID, int instID, string instType)
        {
            memRepo.RemoveMemberSchool(memberID, instID, instType);
        }

        public YoutubeChannel GetYoutubeChannel(int memberID)
        {
            return memRepo.GetYoutubeChannel(memberID);
        }


        public void SetYoutubeChannel(YoutubeChannel body)
        {
            memRepo.SetYoutubeChannel(body);
        }

        public InstagramURL GetInstagramURL(int memberID)
        {
            return memRepo.GetInstagramURL(memberID);
        }

        public void SetInstagramURL(InstagramURL body)
        {
            memRepo.SetInstagramURL(body);
        }

        public List<YoutubePlayList> GetVideoPlayList(int memberId)
        {
            return memRepo.GetVideoPlayList(memberId);
        }

        public List<YoutubeVideosList> GetVideosList(string playerListID)
        {
            return memRepo.GetVideosList(playerListID);
        }

        public void IncrementPostLikeCounter(int postID)
        {
            memRepo.IncrementPostLikeCounter(postID);
        }

        public bool IsFriendByContact(int memberID, int contactID)
        {
            return memRepo.IsFriendByContact(memberID, contactID);
        }

        public bool IsFollowingContact(int memberID, int contactID)
        {
            return memRepo.IsFollowingContact(memberID, contactID);
        }

    }
}