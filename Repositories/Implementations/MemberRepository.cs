using System.Data;
using System.Data.Common;
using dotnet_sp_api.Models.DBContextModels;
using dotnet_sp_api.Models.DTOs;
using dotnet_sp_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;

namespace dotnet_sp_api.Services.Implementations
{
    /// <summary>
    /// Member service functionality code
    /// </summary>
    public class MemberRepository(DBContext context, IConfiguration configuration) : IMemberRepository
    {
        public void CreateMemberPost(int memberID, string postMsg)
        {
            context.Database.ExecuteSqlRaw("SELECT public.sp_create_member_post({0}, {1})", memberID, postMsg);
        }

        public void CreateMemberPostResponse(int memberID, int postID, string postMsg)
        {
            context.Database.ExecuteSqlRaw("SELECT public.sp_create_post_comment({0}, {1}, {2})", memberID, postID, postMsg);
        }

        public List<PostResponse> GetMemberPostResponses(int postID)
        {
            // return memRepo.GetMemberPostResponses(postID);
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                // Use SELECT instead of CommandType.StoredProcedure
                command.CommandText = "SELECT * FROM sp_get_member_child_posts(@post_id)";
                command.CommandType = CommandType.Text;

                // PostgreSQL uses NpgsqlParameter
                var parameter = new NpgsqlParameter("@post_id", NpgsqlTypes.NpgsqlDbType.Integer);
                parameter.Value = postID;
                command.Parameters.Add(parameter);

                var cList = new List<PostResponse>();

                context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pc = new PostResponse
                        {
                            PostResponseID = reader.GetInt32(0),
                            PostID = reader.GetInt32(1),
                            Description = reader.GetString(2),
                            DateResponded = reader.GetString(3),
                            MemberID = reader.GetInt32(4),
                            PicturePath = reader.GetString(5),
                            MemberName = reader.GetString(6),
                            FirstName = reader.GetString(7)
                        };
                        cList.Add(pc);
                    }
                    reader.Close();
                }
                return cList;
            }
        }

        public List<Posts> GetMemberPosts(int memberID)
        {
            var l = (from d in context.Tbcontacts where d.MemberId == memberID select d.ContactId).ToList();
            l.Add(memberID);

            List<Posts> lst;
            lst = (from mn in context.Tbmemberposts
                   join mnn in context.Tbmemberprofiles on mn.MemberId equals mnn.MemberId
                   // where l.Contains((int)mn.MemberId!)
                   orderby mn.PostDate descending
                   select new Posts()
                   {
                       PostID = mn.PostId.ToString(),
                       Title = mn.Title ?? "",
                       Description = mn.Description ?? "",
                       DatePosted = mn.PostDate.ToString() ?? "",
                       AttachFile = mn.AttachFile ?? "",
                       MemberID = mn.MemberId.ToString() ?? "",
                       PicturePath = mnn.PicturePath ?? "default.png",
                       MemberName = mnn.FirstName + " " + mnn.LastName,
                       FirstName = mnn.FirstName ?? "",
                       ChildPostCnt = "0"
                   }).Take(18).ToList();
            return lst;
        }

        public MemberProfileGenInfo GetMemberGeneralInfo(int memberID)
        {
            var mp = (from m in context.Tbmemberprofiles
                      join r in context.Tbinterests on m.InterestedInType equals (int?)r.InterestId into grp
                      from r in grp.DefaultIfEmpty()
                      where m.MemberId == memberID

                      select new MemberProfileGenInfo()
                      {
                          MemberID = m.MemberId.ToString(),
                          FirstName = m.FirstName ?? "",
                          MiddleName = m.MiddleName ?? "",
                          LastName = m.LastName ?? "",
                          Sex = m.Sex ?? "",
                          ShowSexInProfile = m.ShowSexInProfile ?? false,
                          DOBMonth = m.DobMonth ?? "",
                          DOBDay = m.DobDay ?? "",
                          DOBYear = m.DobYear ?? "",
                          ShowDOBType = m.ShowDobType ?? false,
                          Hometown = m.Hometown ?? "",
                          HomeNeighborhood = m.HomeNeighborhood ?? "",
                          CurrentStatus = m.CurrentStatus!.ToString() ?? "",
                          InterestedInType = m.InterestedInType.ToString() ?? "",
                          LookingForEmployment = m.LookingForEmployment ?? false,
                          LookingForRecruitment = m.LookingForRecruitment ?? false,
                          LookingForPartnership = m.LookingForPartnership ?? false,
                          LookingForNetworking = m.LookingForNetworking ?? false,
                          Sport = m.Sport ?? "",
                          Bio = m.Bio ?? "",
                          Height = m.Height ?? "",
                          Weight = m.Weight ?? "",
                          LeftRightHandFoot = m.LeftRightHandFoot ?? "",
                          PreferredPosition = m.PreferredPosition ?? "",
                          SecondaryPosition = m.SecondaryPosition ?? "",
                          PicturePath = m.PicturePath ?? "",
                          JoinedDate = m.JoinedDate.ToString() ?? "",
                          CurrentCity = m.CurrentCity ?? "",
                          TitleDesc = m.TitleDesc ?? "",
                          InterestedDesc = r == null ? String.Empty : r.InterestDesc!
                      }).ToList();

            return mp[0];
        }

        public MemberContactInfo GetMemberContactInfo(int memberID)
        {
            var contact = context.Tbmemberprofilecontactinfos.FirstOrDefault(m => m.MemberId == memberID);

            if (contact == null)
            {
                return new MemberContactInfo
                {
                    Email = string.Empty,
                    OtherEmail = string.Empty,
                    Facebook = string.Empty,
                    Instagram = string.Empty,
                    Twitter = string.Empty,
                    Website = string.Empty,
                    HomePhone = string.Empty,
                    CellPhone = string.Empty,
                    Address = string.Empty,
                    City = string.Empty,
                    Neighborhood = string.Empty,
                    State = string.Empty,
                    Zip = string.Empty,
                    ShowAddress = 0,
                    ShowEmailToMembers = 0,
                    ShowCellPhone = 0,
                    ShowHomePhone = 0
                };
            }

            var mc = new MemberContactInfo
            {
                Email = contact.Email ?? string.Empty,
                OtherEmail = contact.OtherEmail ?? string.Empty,
                Facebook = contact.Facebook ?? string.Empty,
                Instagram = contact.Instagram ?? string.Empty,
                Twitter = contact.Twitter ?? string.Empty,
                Website = contact.Website ?? string.Empty,
                HomePhone = contact.HomePhone ?? string.Empty,
                CellPhone = contact.CellPhone ?? string.Empty,
                Address = contact.Address ?? string.Empty,
                City = contact.City ?? string.Empty,
                Neighborhood = contact.Neighborhood ?? string.Empty,
                State = contact.State ?? string.Empty,
                Zip = contact.Zip ?? string.Empty,
                ShowAddress = (int)(contact.ShowAddress ?? 0),
                ShowEmailToMembers = (int)(contact.ShowEmailToMembers ?? 0),
                ShowCellPhone = (int)(contact.ShowCellPhone ?? 0),
                ShowHomePhone = (int)(contact.ShowHomePhone ?? 0)
            };

            return mc;
        }

        public List<MemberProfileEducation> GetMemberEducationInfo(int memberID)
        {
            var educationList = (from e in context.Tbmemberprofileeducationv2s
                                 join d in context.Tbdegreetypes on e.DegreeType equals d.DegreeTypeId into degreeJoin
                                 from d in degreeJoin.DefaultIfEmpty()

                                 join s in context.Tbschooltypes on e.SchoolType equals s.SchoolTypeId into schoolTypeJoin
                                 from s in schoolTypeJoin.DefaultIfEmpty()

                                 where e.MemberId == memberID
                                 orderby e.ClassYear descending

                                 select new
                                 {
                                     e.MemberId,
                                     e.SchoolId,
                                     e.SchoolType,
                                     e.ClassYear,
                                     e.Major,
                                     e.DegreeType,
                                     e.Societies,
                                     e.Description,
                                     DegreeTypeDesc = d != null ? d.DegreeTypeDesc : "",
                                     e.SportLevelType,
                                     SchoolTypeDesc = s != null ? s.SchoolTypeDesc : ""
                                 }).ToList();

            var result = educationList.Select(e =>
            {
                string schoolName = "";
                string address = "";
                string fileImage = "default.png";

                switch (e.SchoolType)
                {
                    case 3: // College
                        var college = context.Tbcolleges.FirstOrDefault(c => c.SchoolId == e.SchoolId);
                        if (college != null)
                        {
                            schoolName = college.Name ?? "";
                            address = college.Address ?? "";
                            fileImage = college.Website ?? "default.png";
                        }
                        break;

                    case 2: // Private School
                        var priv = context.Tbprivateschools.FirstOrDefault(p => p.LgId == e.SchoolId);
                        if (priv != null)
                        {
                            schoolName = priv.SchoolName ?? "";
                            address = priv.SchoolName ?? ""; // same as SQL
                        }
                        break;

                    case 1: // Public School
                        var pub = context.Tbpublicschools.FirstOrDefault(p => p.Lgid == e.SchoolId);
                        if (pub != null)
                        {
                            schoolName = pub.SchoolName ?? "Unknown Name";
                            address = $"{pub.StreetName}, {pub.City}, {pub.State} {pub.Zip}";
                        }
                        break;
                }

                return new MemberProfileEducation
                {
                    // MemberID = e.MemberId,
                    SchoolID = e.SchoolId.ToString(),
                    SchoolType = e.SchoolType.ToString(),
                    YearClass = e.ClassYear?.ToString() ?? "",
                    Major = e.Major ?? "",
                    DegreeTypeID = e.DegreeType?.ToString() ?? "",
                    Societies = e.Societies ?? "",
                    // Description = e.Description ?? "",
                    Degree = e.DegreeTypeDesc ?? "",
                    SportLevelType = e.SportLevelType ?? "",
                    // SchoolTypeDesc = e.SchoolTypeDesc ?? "",
                    SchoolName = schoolName,
                    SchoolAddress = address,
                    SchoolImage = fileImage
                };
            }).ToList();

            return result;
        }

        public void SaveMemberGeneralInfo(MemberProfileGenInfo input)
        {
            var existing = context.Tbmemberprofiles
                           .FirstOrDefault(m => m.MemberId == Convert.ToInt32(input.MemberID));

            if (existing != null)
            {
                // Update existing member profile
                existing.FirstName = input.FirstName;
                existing.MiddleName = input.MiddleName;
                existing.LastName = input.LastName;
                existing.TitleDesc = input.TitleDesc;
                existing.InterestedInType = Convert.ToInt32(input.InterestedInType);
                existing.CurrentStatus = input.CurrentStatus;
                existing.Sex = input.Sex;
                existing.ShowSexInProfile = input.ShowSexInProfile;
                existing.DobMonth = input.DOBMonth;
                existing.DobDay = input.DOBDay;
                existing.DobYear = input.DOBYear;
                existing.ShowDobType = input.ShowDOBType;
                existing.LookingForPartnership = input.LookingForPartnership;
                existing.LookingForRecruitment = input.LookingForRecruitment;
                existing.LookingForEmployment = input.LookingForEmployment;
                existing.LookingForNetworking = input.LookingForNetworking;
                existing.Sport = input.Sport;
                existing.Bio = input.Bio;
                existing.Height = input.Height;
                existing.Weight = input.Weight;
                existing.LeftRightHandFoot = input.LeftRightHandFoot;
                existing.PreferredPosition = input.PreferredPosition;
                existing.SecondaryPosition = input.SecondaryPosition;

                // Optionally track audit fields (updatedAt, etc.)
            }
            else
            {
                // Insert new member profile
                var newProfile = new Tbmemberprofile
                {
                    MemberId = Convert.ToInt32(input.MemberID),
                    FirstName = input.FirstName,
                    MiddleName = input.MiddleName,
                    LastName = input.LastName,
                    TitleDesc = input.TitleDesc,
                    InterestedInType = Convert.ToInt32(input.InterestedInType),
                    CurrentStatus = input.CurrentStatus,
                    Sex = input.Sex,
                    ShowSexInProfile = input.ShowSexInProfile,
                    DobMonth = input.DOBMonth,
                    DobDay = input.DOBDay,
                    DobYear = input.DOBYear,
                    ShowDobType = input.ShowDOBType,
                    LookingForPartnership = input.LookingForPartnership,
                    LookingForRecruitment = input.LookingForRecruitment,
                    LookingForEmployment = input.LookingForEmployment,
                    LookingForNetworking = input.LookingForNetworking,
                    Sport = input.Sport,
                    Bio = input.Bio,
                    Height = input.Height,
                    Weight = input.Weight,
                    LeftRightHandFoot = input.LeftRightHandFoot,
                    PreferredPosition = input.PreferredPosition,
                    SecondaryPosition = input.SecondaryPosition
                };

                context.Tbmemberprofiles.Add(newProfile);
            }

            context.SaveChanges();
        }

        public void SaveMemberContactInfo(int memberID, MemberContactInfo input)
        {
            var existing = context.Tbmemberprofilecontactinfos
                          .FirstOrDefault(m => m.MemberId == memberID);

            if (existing != null)
            {
                // Update existing contact info
                existing.Email = input.Email;
                existing.ShowEmailToMembers = input.ShowEmailToMembers;
                existing.OtherEmail = input.OtherEmail;

                existing.Facebook = input.Facebook;
                existing.Instagram = input.Instagram;
                existing.Twitter = input.Twitter;
                existing.Website = input.Website;

                existing.CellPhone = input.CellPhone;
                existing.ShowCellPhone = input.ShowCellPhone;
                existing.HomePhone = input.HomePhone;
                existing.ShowHomePhone = input.ShowHomePhone;

                existing.Address = input.Address;
                existing.ShowAddress = input.ShowAddress;
                existing.City = input.City;
                existing.State = input.State;
                existing.Zip = input.Zip;
            }
            else
            {
                // Insert new contact info
                var newContact = new Tbmemberprofilecontactinfo
                {
                    MemberId = memberID,
                    Email = input.Email,
                    ShowEmailToMembers = input.ShowEmailToMembers,
                    OtherEmail = input.OtherEmail,

                    Facebook = input.Facebook,
                    Instagram = input.Instagram,
                    Twitter = input.Twitter,
                    Website = input.Website,

                    CellPhone = input.CellPhone,
                    ShowCellPhone = input.ShowCellPhone,
                    HomePhone = input.HomePhone,
                    ShowHomePhone = input.ShowHomePhone,

                    Address = input.Address,
                    ShowAddress = input.ShowAddress,
                    City = input.City,
                    State = input.State,
                    Zip = input.Zip
                };

                context.Tbmemberprofilecontactinfos.Add(newContact);
            }
            context.SaveChanges();
        }

        public void AddMemberSchool(int memberID, MemberProfileEducation body)
        {
            Tbmemberprofileeducationv2 mp = new()
            {
                MemberId = memberID,
                SchoolId = Convert.ToInt32(body.SchoolID),
                SchoolType = int.TryParse(body.SchoolType, out int schoolType) ? schoolType : 0,
                SchoolName = body.SchoolName,
                ClassYear = body.YearClass,
                Major = body.Major,
                DegreeType = int.TryParse(body.DegreeTypeID, out int degreeType) ? degreeType : 0,
                Societies = body.Societies,
                SportLevelType = body.SportLevelType
            };
            context.Tbmemberprofileeducationv2s.Add(mp);
            context.SaveChanges();
        }

        public void UpdateMemberSchool(int memberID, MemberProfileEducation body)
        {
            //update tb meber profile with new profile picture
            var lst = (from m in context.Tbmemberprofileeducationv2s where m.MemberId == memberID && m.SchoolId == Convert.ToInt32(body.SchoolID) && m.SchoolType == Convert.ToInt32(body.SchoolType) select m).ToList();

            if (lst.Count != 0)
            {
                var mbr = lst.First();
                mbr.ClassYear = body.YearClass;
                mbr.Major = body.Major;
                mbr.DegreeType = Convert.ToInt32(body.DegreeTypeID);
                mbr.Societies = body.Societies;
                mbr.SportLevelType = body.SportLevelType;
                context.SaveChanges();
            }
        }

        public void RemoveMemberSchool(int memberID, int instID, string instType)
        {
            var s = (from c in context.Tbmemberprofileeducationv2s where c.SchoolId == instID && c.MemberId == memberID && c.SchoolType == Convert.ToInt32(instType) select c).First();
            context.Tbmemberprofileeducationv2s.Remove(s);
            context.SaveChanges();
        }

        public YoutubeChannel GetYoutubeChannel(int memberID)
        {
            YoutubeChannel yc = new();

            var s = (from m in context.Tbmembers where m.MemberId == memberID select m).ToList();
            if (s != null)
            {
                yc.ChannelID = s[0].YoutubeChannel!;
                yc.MemberID = memberID.ToString();
                return yc;
            }
            else
                return yc;
        }

        public void SetYoutubeChannel(YoutubeChannel body)
        {
            var mbr = (from m in context.Tbmembers where m.MemberId == Convert.ToInt32(body.MemberID) select m).First();
            if (mbr != null)
            {
                mbr.YoutubeChannel = body.ChannelID;
                context.SaveChanges();
            }
        }

        public InstagramURL GetInstagramURL(int memberID)
        {
            InstagramURL iu = new();
            var s = (from m in context.Tbmemberprofilecontactinfos where m.MemberId == memberID select m).ToList();
            if (s != null)
            {
                iu.MemberID = memberID.ToString(); iu.Url = s[0].Instagram ?? "";
                return iu;
            }
            else
                return iu;
        }

        public void SetInstagramURL(InstagramURL body)
        {
            var mbr = (from m in context.Tbmemberprofilecontactinfos where m.MemberId == Convert.ToInt32(body.MemberID) select m).First();
            if (mbr != null)
            {
                mbr.Instagram = body.Url;
                context.SaveChanges();
            }
        }

        public List<YoutubePlayList> GetVideoPlayList(int memberId)
        {
            List<YoutubePlayList> lst = new List<YoutubePlayList>();
            try
            {
                var cInfo = GetYoutubeChannel(memberId);

                if (!string.IsNullOrEmpty(cInfo.ChannelID))
                {

                    string apiKey = configuration.GetValue<string>("YoutubeAPIkey") ?? "";

                    YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = apiKey });
                    var pl = yt.Playlists.List("snippet");
                    pl.ChannelId = cInfo.ChannelID;
                    var plResult = pl.Execute();
                    foreach (var item in plResult.Items)
                    {
                        YoutubePlayList plModel = new YoutubePlayList();
                        plModel.Id = item.Id;
                        plModel.Title = item.Snippet.Title;
                        plModel.Description = item.Snippet.Description;
                        plModel.Etag = item.ETag;
                        plModel.DefaultThumbnail = item.Snippet.Thumbnails.Default__.Url;
                        plModel.DefaultThumbnailHeight = item.Snippet.Thumbnails.Default__.Height.ToString() ?? "";
                        plModel.DefaultThumbnailWidth = item.Snippet.Thumbnails.Default__.Width.ToString() ?? "";
                        lst.Add(plModel);
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return lst;
            }
        }

        public List<YoutubeVideosList> GetVideosList(string playerListID)
        {
            List<YoutubeVideosList> lst = new List<YoutubeVideosList>();
            string apiKey = configuration.GetValue<string>("YoutubeAPIkey") ?? "";
            YouTubeService yt = new(new BaseClientService.Initializer() { ApiKey = apiKey });
            var vids = yt.PlaylistItems.List("snippet");
            vids.PlaylistId = playerListID;
            var vidResult = vids.Execute();
            foreach (var item in vidResult.Items)
            {
                YoutubeVideosList vidModel = new YoutubeVideosList();
                vidModel.Id = item.Snippet.ResourceId.VideoId;
                vidModel.Title = item.Snippet.Title;
                vidModel.Description = item.Snippet.Description;
                vidModel.Etag = item.ETag;
                vidModel.PublishedAt = item.Snippet.PublishedAtDateTimeOffset?.DateTime.ToShortDateString();
                //vidModel.PublishedAt = Convert.ToDateTime(item.Snippet.PublishedAtDateTimeOffset).ToShortDateString();
                vidModel.DefaultThumbnail = item.Snippet.Thumbnails.Default__.Url;
                vidModel.DefaultThumbnailHeight = item.Snippet.Thumbnails.Default__.Height.ToString() ?? "0";
                vidModel.DefaultThumbnailWidth = item.Snippet.Thumbnails.Default__.Width.ToString() ?? "0";
                lst.Add(vidModel);
            }
            return lst;
        }

        public void IncrementPostLikeCounter(int postID)
        {
            var post = context.Tbmemberposts.FirstOrDefault(p => p.PostId == postID);
            if (post != null)
            {
                post.LikeCounter = (post.LikeCounter ?? 0) + 1;
                context.SaveChanges();
            }
        }

        public bool IsFriendByContact(int memberID, int contactID)
        {
            var clist = (from m in context.Tbmembers join c in context.Tbcontacts on m.MemberId equals c.ContactId where c.MemberId == memberID && c.ContactId == contactID select m).ToList();
            if (clist.Count == 0)
                return false;
            else
                return true;
        }

        public bool IsFollowingContact(int memberID, int contactID)
        {
            var result = (from f in context.Tbmemberfollowings
                          where f.MemberId == memberID && f.FollowingMemberId == contactID
                          select f.MemberId).FirstOrDefault();

            if (result == 0)
                return false;
            else
                return true;
        }

    }
}