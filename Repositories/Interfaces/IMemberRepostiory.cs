
using dotnet_sp_api.Models.DTOs;

namespace dotnet_sp_api.Services.Interfaces
{
    public interface IMemberRepository
    {
        void CreateMemberPost(int memberID, string postMsg);
        void CreateMemberPostResponse(int memberID, int postID, string postMsg);
        List<PostResponse> GetMemberPostResponses(int postID);
        List<Posts> GetMemberPosts(int memberID);
        MemberProfileGenInfo GetMemberGeneralInfo(int memberID);
        MemberContactInfo GetMemberContactInfo(int memberID);
        List<MemberProfileEducation> GetMemberEducationInfo(int memberID);
        void SaveMemberGeneralInfo(MemberProfileGenInfo body);
        void SaveMemberContactInfo(int memberID, MemberContactInfo body);
        void AddMemberSchool(int memberID, MemberProfileEducation body);
        void UpdateMemberSchool(int memberID, MemberProfileEducation body);
        void RemoveMemberSchool(int memberID, int instID, string instType);
        YoutubeChannel GetYoutubeChannel(int emberID);
        void SetYoutubeChannel(YoutubeChannel body);
        InstagramURL GetInstagramURL(int memberID);
        void SetInstagramURL(InstagramURL body);
        List<YoutubePlayList> GetVideoPlayList(int memberId);
        List<YoutubeVideosList> GetVideosList(string playerListID);
        bool IsFriendByContact(int memberID, int contactID);
        bool IsFollowingContact(int memberID, int contactID);
        void IncrementPostLikeCounter(int postID);
    }
}