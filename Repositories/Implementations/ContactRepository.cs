using Microsoft.EntityFrameworkCore;
using System.Data;
using dotnet_sp_api.Models.DBContextModels;
using dotnet_sp_api.Models.DTOs;
using dotnet_sp_api.Repositories.Interfaces;
using Microsoft.OpenApi.Expressions;

namespace dotnet_sp_api.Repositories.Implementations
{
    /// <summary>
    /// Describes the functionalities for accessing data for contacts.
    /// </summary>
    public class ContactRepository(DBContext context, IMessageRepository msgRepo) : IContactRepository
    {
        readonly DBContext _context = context;

        /// <summary>
        /// Get list of member contacts.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<MemberContacts> GetMemberContacts(int memberID)
        {
            var cList = new List<MemberContacts>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM public.sp_get_member_contacts(@p0, @p1)";
                command.CommandType = CommandType.Text;

                var memberIdParam = new Npgsql.NpgsqlParameter("p0", memberID);
                var showTypeParam = new Npgsql.NpgsqlParameter("p1", ""); // Or pass a real value

                command.Parameters.Add(memberIdParam);
                command.Parameters.Add(showTypeParam);

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pc = new MemberContacts
                        {
                            ContactID = reader["contact_member_id"]?.ToString()!,
                            Email = reader["email"]?.ToString()!,
                            FirstName = reader["first_name"]?.ToString()!,
                            FriendName = reader["friend_name"]?.ToString()!,
                            LabelText = reader["label_text"]?.ToString()!,
                            Location = reader["location"]?.ToString()!,
                            NameAndID = reader["name_and_id"]?.ToString()!,
                            Params = reader["params"]?.ToString()!,
                            ParamsAV = reader["paramsav"]?.ToString()!,
                            PicturePath = reader["picture_path"]?.ToString()!,
                            ShowType = reader["show_type"]?.ToString()!,
                            Status = reader["status"]?.ToString()!,
                            TitleDesc = reader["title_desc"]?.ToString()!
                        };
                        cList.Add(pc);
                    }
                }
                _context.Database.CloseConnection();
            }
            return cList;
        }

        public List<MemberContacts> GetMemberSuggestions(int memberID)
        {
            var cList = new List<MemberContacts>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                // PostgreSQL functions are called with SELECT
                command.CommandText = "SELECT * FROM public.sp_get_member_suggestions(@p0)";
                command.CommandType = CommandType.Text;

                var memberIdParam = new Npgsql.NpgsqlParameter("p0", memberID);
                command.Parameters.Add(memberIdParam);

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pc = new MemberContacts
                        {
                            ContactID = reader["contact_id"]?.ToString()!,
                            Email = reader["email"]?.ToString()!,
                            FirstName = reader["first_name"]?.ToString()!,
                            FriendName = reader["friend_name"]?.ToString()!,
                            LabelText = reader["label_text"]?.ToString()!,
                            Location = reader["location"]?.ToString()!,
                            NameAndID = reader["name_and_id"]?.ToString()!,
                            Params = reader["params"]?.ToString()!,
                            ParamsAV = reader["paramsav"]?.ToString()!,
                            PicturePath = reader["picture_path"]?.ToString()!,
                            ShowType = reader["show_type"]?.ToString()!,
                            Status = reader["status"]?.ToString()!,
                            TitleDesc = reader["title_desc"]?.ToString()!,
                            ShowFollow = reader["show_follow"]?.ToString()!
                        };

                        cList.Add(pc);
                    }
                }
                _context.Database.CloseConnection();
            }
            return cList;
        }

        /// <summary>
        /// get search member contacts by search text wildcard
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<MemberContacts> SearchMemberContacts(int memberID, string searchText)
        {
            var lst = (from mpf in _context.Tbmemberprofiles
                       join ct in _context.Tbcontacts on mpf.MemberId equals ct.ContactId
                       join mcti in _context.Tbmemberprofilecontactinfos on ct.ContactId equals mcti.MemberId
                       into t
                       from nt in t.DefaultIfEmpty()
                       where
                       ct.MemberId == memberID && (ct.Status == 0 || ct.Status == 1) &&
                       (mpf.FirstName!.Contains(searchText) || mpf.LastName!.Contains(searchText))
                       select new MemberContacts()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName ?? "",
                           Location = nt.City + ", " + nt.State,
                           PicturePath = string.IsNullOrEmpty(mpf.PicturePath) ? "default.png" : mpf.PicturePath,
                           ContactID = ct.ContactId.ToString(),
                           ShowType = "",
                           Status = ct.Status.ToString(),
                           TitleDesc = mpf.TitleDesc ?? ""
                       }).ToList();
            return lst;
        }

        /// <summary>
        /// Get the list of contact requests.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public List<MemberContacts> GetRequestsList(int memberID)
        {
            var lst = (from mpf in _context.Tbmemberprofiles
                       join c in _context.Tbcontactrequests on mpf.MemberId equals c.FromMemberId
                       where c.ToMemberId == memberID && (c.Status == 0)
                       select new MemberContacts()
                       {
                           FriendName = mpf.FirstName + " " + mpf.LastName,
                           FirstName = mpf.FirstName ?? "",
                           PicturePath = string.IsNullOrEmpty(mpf.PicturePath) ? "default.png" : mpf.PicturePath,
                           ContactID = c.FromMemberId.ToString() ?? "",

                       }).ToList();
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
            var contacts = new List<MemberContacts>();

            using (var connection = _context.Database.GetDbConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM public.sp_get_search_contacts(@UserID, @SearchText, @SearchText2)";
                    command.CommandType = CommandType.Text;

                    // Create parameters
                    var userIdParam = command.CreateParameter();
                    userIdParam.ParameterName = "@UserID";
                    userIdParam.Value = userID;
                    command.Parameters.Add(userIdParam);

                    var searchTextParam = command.CreateParameter();
                    searchTextParam.ParameterName = "@SearchText";
                    searchTextParam.Value = searchText;
                    command.Parameters.Add(searchTextParam);

                    var searchText2Param = command.CreateParameter();
                    searchText2Param.ParameterName = "@SearchText2";
                    searchText2Param.Value = ""; // Or pass another value as needed
                    command.Parameters.Add(searchText2Param);

                    // Open the connection if it's not already open
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    // Execute and read the results
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var contact = new MemberContacts
                            {
                                ContactID = reader["connection_id"]?.ToString() ?? "",
                                Email = reader["email"]?.ToString() ?? "",
                                FirstName = reader["first_name"]?.ToString() ?? "",
                                FriendName = reader["friend_name"]?.ToString() ?? "",
                                LabelText = reader["label_text"]?.ToString() ?? "",
                                Location = reader["location"]?.ToString() ?? "",
                                NameAndID = reader["name_and_id"]?.ToString() ?? "",
                                Params = reader["params"]?.ToString() ?? "",
                                ParamsAV = reader["paramsav"]?.ToString() ?? "",
                                PicturePath = reader["picture_path"]?.ToString() ?? "",
                                ShowType = reader["show_type"]?.ToString() ?? "",
                                Status = reader["status"]?.ToString() ?? "",
                                TitleDesc = reader["title_desc"]?.ToString() ?? "",
                                ShowFollow = reader["show_type"]?.ToString() ?? ""
                            };
                            contacts.Add(contact);
                        }
                        reader.Close();
                    }
                }
            }
            return contacts;
        }

        /// <summary>
        /// Accept contact's request 
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="contactID"></param>
        public void AcceptRequest(int memberID, int contactID)
        {
            var sql = "SELECT public.sp_accept_request(@MemberID, @ContactID)";

            var memberIdParam = new Npgsql.NpgsqlParameter("@MemberID", memberID);
            var contactIdParam = new Npgsql.NpgsqlParameter("@ContactID", contactID);

            _context.Database.ExecuteSqlRaw(sql, memberIdParam, contactIdParam);
        }

        /// <summary>
        /// Reject contact's request
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="contactID"></param>
        public void RejectRequest(int memberID, int contactID)
        {
            var sql = "SELECT public.sp_reject_request(@MemberID, @ContactID)";

            var memberIdParam = new Npgsql.NpgsqlParameter("@MemberID", memberID);
            var contactIdParam = new Npgsql.NpgsqlParameter("@ContactID", contactID);

            _context.Database.ExecuteSqlRaw(sql, memberIdParam, contactIdParam);
        }

        /// <summary>
        /// Delete contact
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="contactID"></param>
        public void DeleteContact(int memberID, int contactID)
        {
            var sql = "SELECT public.sp_delete_contact(@MemberID, @ContactID)";

            var memberIdParam = new Npgsql.NpgsqlParameter("@MemberID", memberID);
            var contactIdParam = new Npgsql.NpgsqlParameter("@ContactID", contactID);

            _context.Database.ExecuteSqlRaw(sql, memberIdParam, contactIdParam);

        }

        /// <summary>
        /// Get whose following me
        /// </summary>
        /// <returns>The list of members I am following.</returns>
        /// <param name="memberID">User identifier.</param>
        public List<MemberContacts> GetWhosFollowingMe(int memberID)
        {
            var contacts = new List<MemberContacts>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM public.sp_get_whose_following_me(@MemberID)";
                command.CommandType = CommandType.Text;

                var memberIdParam = new Npgsql.NpgsqlParameter("@MemberID", memberID);
                command.Parameters.Add(memberIdParam);

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contact = new MemberContacts
                        {
                            ContactID = reader["connection_id"]?.ToString() ?? "",
                            Email = reader["email"]?.ToString() ?? "",
                            FirstName = reader["first_name"]?.ToString() ?? "",
                            FriendName = reader["friend_name"]?.ToString() ?? "",
                            LabelText = reader["label_text"]?.ToString() ?? "",
                            Location = reader["location"]?.ToString() ?? "",
                            NameAndID = reader["name_and_id"]?.ToString() ?? "",
                            Params = reader["params"]?.ToString() ?? "",
                            ParamsAV = reader["paramsav"]?.ToString() ?? "",
                            PicturePath = reader["picture_path"]?.ToString() ?? "",
                            ShowType = reader["show_type"]?.ToString() ?? "",
                            Status = reader["status"]?.ToString() ?? "",
                            TitleDesc = reader["title_desc"]?.ToString() ?? "",
                            ShowFollow = reader["show_type"]?.ToString() ?? ""
                        };

                        contacts.Add(contact);
                    }
                }

                _context.Database.CloseConnection();
            }
            return contacts;
        }

        /// <summary>
        /// get people I follow
        /// </summary>
        /// <param name="memberID">User identifier.</param>
        public List<MemberContacts> GetPeopleIFollow(int memberID)
        {
            var contacts = new List<MemberContacts>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM public.sp_get_followed_members(@MemberID)";
                command.CommandType = CommandType.Text;

                var memberIdParam = new Npgsql.NpgsqlParameter("@MemberID", memberID);
                command.Parameters.Add(memberIdParam);

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contact = new MemberContacts
                        {
                            ContactID = reader["contact_id"]?.ToString() ?? "",
                            Email = reader["email"]?.ToString() ?? "",
                            FirstName = reader["first_name"]?.ToString() ?? "",
                            FriendName = reader["friend_name"]?.ToString() ?? "",
                            LabelText = reader["labeltext"]?.ToString() ?? "",
                            Location = reader["location"]?.ToString() ?? "",
                            NameAndID = reader["name_and_id"]?.ToString() ?? "",
                            Params = reader["params"]?.ToString() ?? "",
                            ParamsAV = reader["paramsav"]?.ToString() ?? "",
                            PicturePath = reader["picture_path"]?.ToString() ?? "",
                            ShowType = reader["show_type"]?.ToString() ?? "",
                            Status = reader["status"]?.ToString() ?? "",
                            TitleDesc = reader["title_desc"]?.ToString() ?? "",
                            ShowFollow = reader["show_type"]?.ToString() ?? ""
                        };

                        contacts.Add(contact);
                    }
                }
                _context.Database.CloseConnection();
            }
            return contacts;
        }

        /// <summary>
        /// follow member
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="contactID"></param>
        public void FollowMember(int memberID, int contactID)
        {
            var sql = "SELECT public.sp_add_following_member(@MemberID, @ContactID)";
            var memberIdParam = new Npgsql.NpgsqlParameter("@MemberID", memberID);
            var contactIdParam = new Npgsql.NpgsqlParameter("@ContactID", contactID);
            _context.Database.ExecuteSqlRaw(sql, memberIdParam, contactIdParam);
        }

        /// <summary>
        /// un follow member
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="followingMemberId"></param>
        public void UnfollowMember(int memberID, int followingMemberId)
        {
            var sql = "SELECT public.sp_unfollow_member(@MemberID, @FollowingMemberID)";
            var memberIdParam = new Npgsql.NpgsqlParameter("@MemberID", memberID);
            var followingMemberIdParam = new Npgsql.NpgsqlParameter("@FollowingMemberID", followingMemberId);
            _context.Database.ExecuteSqlRaw(sql, memberIdParam, followingMemberIdParam);
        }

        /// <summary>
        /// Get contact search results 
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<ContactSearch> ContactSearchResults(int memberID, string searchText)
        {
            List<ContactSearch>? lst = null;
            lst = (from mpf in _context.Tbmemberprofiles
                   join c in _context.Tbcontacts on mpf.MemberId equals c.MemberId
                   join p in _context.Tbmemberprofilecontactinfos on c.MemberId equals p.MemberId
                   where c.MemberId == memberID && (mpf.LastName!.Contains(searchText) || mpf.FirstName!.Contains(searchText))

                   select new ContactSearch()
                   {
                       EntityID = c.ContactId.ToString(),
                       EntityName = mpf.FirstName + " " + mpf.LastName,
                       FirstName = mpf.FirstName ?? "",
                       LastName = mpf.LastName ?? "",
                       PicturePath = (string.IsNullOrEmpty(mpf.PicturePath)) ? "default.png" : mpf.PicturePath,
                       CityState = p.City + ", " + p.State,
                       LabelText = "",
                       Email = "",
                       NameAndID = "",
                       Params = "",
                       ParamsAV = "",
                       Description = "",
                       MemberCount = "",
                       CreatedDate = "01/01/1900",
                       Location = "",
                   }
                      ).ToList();
            return lst;
        }

        public List<Search> SearchResults(int memberID, string searchText)
        {
            var results = new List<Search>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                // Adjust function name & schema if needed
                command.CommandText = "SELECT * FROM public.sp_search_results(@MemberID, @SearchText)";
                command.CommandType = CommandType.Text;

                // Add PostgreSQL parameters (Npgsql is typically used under the hood)
                var memberIdParam = new Npgsql.NpgsqlParameter("@MemberID", memberID);
                var searchTextParam = new Npgsql.NpgsqlParameter("@SearchText", searchText);
                command.Parameters.Add(memberIdParam);
                command.Parameters.Add(searchTextParam);

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Search
                        {
                            EntityID = reader["entity_id"] != DBNull.Value ? Convert.ToInt32(reader["entity_id"]) : 0,
                            EntityName = reader["entity_name"]?.ToString() ?? "",
                            PicturePath = reader["picture_path"]?.ToString() ?? "",
                            Location = reader["location"]?.ToString() ?? "",
                            EventDate = "",
                            Rsvp = "",
                            Params = reader["params"]?.ToString() ?? "",
                            Description = reader["description"]?.ToString() ?? "",
                            MemberCount = reader["member_count"]?.ToString() ?? "",
                            CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : DateTime.MinValue,
                            CityState = reader["city_state"]?.ToString() ?? "",
                            Email = reader["email"]?.ToString() ?? "",
                            NameAndID = reader["name_and_id"]?.ToString() ?? "",
                            StartDate = reader["start_date"] != DBNull.Value ? Convert.ToDateTime(reader["start_date"]) : DateTime.MinValue,
                            EndDate = reader["enddate"] != DBNull.Value ? Convert.ToDateTime(reader["enddate"]) : DateTime.MinValue,
                            LabelText = reader["label_text"]?.ToString() ?? "",
                            ShowCancel = reader["show_cancel"]?.ToString() ?? "",
                            ParamsAV = reader["paramsav"]?.ToString() ?? "",
                            Stype = reader["stype"]?.ToString() ?? ""
                        };

                        results.Add(item);
                    }
                }
                _context.Database.CloseConnection();
            }
            return results;
        }

        public void SendRequestContact(int memberID, int contactID)
        {
            var existingRequest = _context.Tbcontactrequests.FirstOrDefault(cr => cr.FromMemberId == memberID && cr.ToMemberId == contactID);
            if (existingRequest == null)
            {
                var newRequest = new Tbcontactrequest
                {
                    FromMemberId = memberID,
                    ToMemberId = contactID,
                    Status = 0,
                    RequestDate = DateTime.Now
                };
                _context.Tbcontactrequests.Add(newRequest);
                var rowsInserted = _context.SaveChanges();

                // Create and initialize the object
                var message = new MessageInputs
                {
                    to = contactID.ToString(),
                    from = memberID.ToString(),
                    subject = "Requesting Contact",
                    body = "I would like to add you to my contact list so we can start networking. Please accept the request from your request contacts list.",
                    attachement = "",
                    original_msg = "",
                    message_id = "0",
                    sent_date = DateTime.Now.ToString(),
                    sender_picture = ""
                };

                msgRepo.CreateMessage(message);
            }
        }
    }
}