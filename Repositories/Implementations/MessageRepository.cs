using System.Data;
using dotnet_sp_api.Models.DBContextModels;
using dotnet_sp_api.Repositories.Interfaces;
using dotnet_sp_api.Models.DTOs;

namespace dotnet_sp_api.Repositories.Implementations
{
    /// <summary>
    /// Describes the functionalities for member and network messages.
    /// </summary>
    public class MessageRepository(DBContext context) : IMessageRepository
    {
        readonly DBContext _context = context;

        public enum MessageStatus { UnRead, Read, Forwarded, RepliedTo };
        public enum MessageMoveType { Deleted, Junk, Sent, Message };
        public enum MessageFolder { Inbox, Junk, Sent, Deleted };

        public List<SearchMessages> GetMemberMessages(int memberID, string showType)
        {
            List<SearchMessages> msgList = [];
            if (showType != "All" && showType != "UnRead")
            {
                msgList = (
                    from msg in _context.Tbmessages
                    join mpf in _context.Tbmemberprofiles on msg.SenderId equals mpf.MemberId
                    into t
                    from nt in t.DefaultIfEmpty()
                    where
                        msg.ContactId == memberID &&
                        (nt.FirstName!.Contains(showType) || nt.LastName!.Contains(showType) || msg.Body!.Contains(showType))
                    orderby msg.MsgDate descending
                    select new SearchMessages()
                    {
                        Attachement = msg.Attachment.ToString() ?? "",
                        Body = (msg.Body!.Length > 35) ? msg.Body.Substring(0, Math.Min(msg.Body.Length, 33)) + "..." : msg.Body,
                        ContactName = nt.FirstName + " " + nt.LastName,
                        ContactImage = Getpicturepath(Convert.ToInt32(msg.ContactId), _context),
                        SenderImage = string.IsNullOrEmpty(nt.PicturePath) ? "default.png" : nt.PicturePath,
                        ContactID = memberID.ToString(),
                        FlagLevel = msg.FlagLevel.ToString() ?? "",
                        ImportanceLevel = msg.ImportanceLevel.ToString() ?? "",
                        MessageID = msg.MessageId.ToString(),
                        MessageState = msg.MessageState.ToString(),
                        SenderID = nt.FirstName + " " + nt.LastName,
                        Subject = msg.Subject ?? "",
                        MsgDate = msg.MsgDate.ToString() ?? "",
                        FromID = msg.SenderId.ToString() ?? "",
                        FirstName = nt.FirstName ?? "",
                        FullBody = msg.Body
                    }
                ).ToList();
            }
            else if (showType == "All")
            {
                msgList = (
                    from msg in _context.Tbmessages
                    join mpf in _context.Tbmemberprofiles on msg.SenderId equals mpf.MemberId
                    into t
                    from nt in t.DefaultIfEmpty()
                    where
                           msg.ContactId == memberID
                    orderby msg.MsgDate descending
                    select new SearchMessages()
                    {
                        Attachement = msg.Attachment.ToString() ?? "",
                        Body = (msg.Body!.Length > 35) ? msg.Body.Substring(0, Math.Min(msg.Body.Length, 33)) + "..." : msg.Body,
                        ContactName = nt.FirstName + " " + nt.LastName,
                        ContactImage = Getpicturepath(Convert.ToInt32(msg.ContactId), _context),
                        SenderImage = string.IsNullOrEmpty(nt.PicturePath) ? "default.png" : nt.PicturePath,
                        ContactID = memberID.ToString(),
                        FlagLevel = msg.FlagLevel.ToString() ?? "",
                        ImportanceLevel = msg.ImportanceLevel.ToString() ?? "",
                        MessageID = msg.MessageId.ToString(),
                        MessageState = msg.MessageState.ToString(),
                        SenderID = nt.FirstName + " " + nt.LastName,
                        Subject = msg.Subject ?? "",
                        MsgDate = msg.MsgDate.ToString() ?? "",
                        FromID = msg.SenderId.ToString() ?? "",
                        FirstName = nt.FirstName ?? "",
                        FullBody = msg.Body
                    }
                ).ToList();
            }
            else
            {
                msgList = (
                    from msg in _context.Tbmessages
                    join mpf in _context.Tbmemberprofiles on msg.SenderId equals mpf.MemberId
                    into t
                    from nt in t.DefaultIfEmpty()
                    where
                        msg.ContactId == memberID && msg.MessageState == 0
                    orderby msg.MsgDate descending
                    select new SearchMessages()
                    {
                        Attachement = msg.Attachment.ToString() ?? "",
                        Body = (msg.Body!.Length > 35) ? msg.Body.Substring(0, Math.Min(msg.Body.Length, 33)) + "..." : msg.Body,
                        ContactName = nt.FirstName + " " + nt.LastName,
                        ContactImage = Getpicturepath(Convert.ToInt32(msg.ContactId), _context),
                        SenderImage = string.IsNullOrEmpty(nt.PicturePath) ? "default.png" : nt.PicturePath,
                        ContactID = memberID.ToString(),
                        FlagLevel = msg.FlagLevel.ToString() ?? "",
                        ImportanceLevel = msg.ImportanceLevel.ToString() ?? "",
                        MessageID = msg.MessageId.ToString(),
                        MessageState = msg.MessageState.ToString(),
                        SenderID = nt.FirstName + " " + nt.LastName,
                        Subject = msg.Subject ?? "",
                        MsgDate = msg.MsgDate.ToString() ?? "",
                        FromID = msg.SenderId.ToString() ?? "",
                        FirstName = nt.FirstName ?? "",
                        FullBody = msg.Body
                    }
                ).ToList();
            }
            return msgList;
        }


        /// <summary>
        /// Get total unread messages  for member id
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public int GetTotalUnreadMessages(int memberID)
        {
            var q = (from p in _context.Tbmessages where p.ContactId == memberID && p.MessageState == 0 select p.MessageId).Count();
            return (q);
        }

        /// <summary>
        /// create a message 
        /// </summary>
        /// <param name="body"></param>
        public void CreateMessage(MessageInputs body)
        {
            //create a new message object
            Tbmessage m = new Tbmessage();

            m.SenderId = long.Parse(body.from);
            m.ContactId = long.Parse(body.to);
            m.Subject = body.subject;
            m.MsgDate = DateTime.Now;
            m.Body = body.body;

            if (string.IsNullOrEmpty(body.attachement))
            {
                m.Attachment = 0;
            }
            else
            {
                m.Attachment = 1;
            }

            m.MessageState = 0;
            m.AttachmentFile = body.attachement;
            m.FlagLevel = 0;
            m.ImportanceLevel = 0;
            m.OriginalMsg = body.original_msg;

            // Add the new object to the the table.
            _context.Tbmessages.Add(m); ;

            // submit and save the new message to both tables
            _context.SaveChanges();
        }

        /// <summary>
        /// create a message 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachment"></param>
        /// <param name="source"></param>
        /// <param name="original"></param>
        public void CreateMessage(string to, string from, string subject, string body, string attachment, string source, string original)
        {
            //create a new message object
            Tbmessage m = new Tbmessage();

            m.SenderId = Convert.ToInt32(from);
            m.ContactId = Convert.ToInt32(to);
            m.Subject = subject;
            m.MsgDate = DateTime.Now;
            m.Body = body;

            if (string.IsNullOrEmpty(attachment))
            {
                m.Attachment = 0;
            }
            else
            {
                m.Attachment = 1;
            }

            m.MessageState = 0;
            m.AttachmentFile = attachment;
            m.FlagLevel = 0;
            m.ImportanceLevel = 0;
            m.OriginalMsg = original;
            m.Source = source;

            // Add the new object to the the table.
            _context.Tbmessages.Add(m);

            // submit and save the new message to both tables
            _context.SaveChanges();
        }

        /// <summary>
        /// Toggle message state 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msgID"></param>
        public void ToggleMessageState(int status, int msgID)
        {
            switch (status)
            {
                case (int)MessageStatus.UnRead:
                    PerformMessageStatus(0, msgID, _context);
                    break;
                case (int)MessageStatus.Read:
                    PerformMessageStatus(1, msgID, _context);
                    break;
                case (int)MessageStatus.Forwarded:
                    PerformMessageStatus(2, msgID, _context);
                    break;
                case (int)MessageStatus.RepliedTo:
                    PerformMessageStatus(3, msgID, _context);
                    break;
            }
        }

        /// <summary>
        /// Perform message status
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msgID"></param>
        /// <param name="context"></param>
        private static void PerformMessageStatus(int status, int msgID, DBContext context)
        {
            var msg = (from m in context.Tbmessages where (m.MessageId == msgID) select m).First();
            msg.MessageState = status;
            context.SaveChanges();
        }

        /// <summary>
        /// Get message info by message ID
        /// </summary>
        /// <param name="msgID"></param>
        /// <returns></returns>
        public List<MessageInfo> GetMessageInfoByID(int msgID)
        {
            List<MessageInfo> lst;
            lst = (from msg in _context.Tbmessages
                   join tmp in _context.Tbmemberprofiles on msg.SenderId equals tmp.MemberId
                   where msg.MessageId == msgID
                   select new MessageInfo()
                   {
                       MessageID = msg.MessageId.ToString(),
                       SentDate = msg.MsgDate.ToString() ?? "",
                       From = tmp.FirstName + " " + tmp.LastName,
                       SenderPicture = string.IsNullOrEmpty(tmp.PicturePath) ? "default.png" : tmp.PicturePath,
                       Body = msg.Body ?? "",
                       Subject = msg.Subject ?? "",
                       AttachmentFile = msg.AttachmentFile ?? "",
                       OrginalMsg = msg.OriginalMsg ?? ""
                   }
                  ).ToList();
            return lst;
        }

        /// <summary>
        /// Delete message id
        /// </summary>
        /// <param name="msgID"></param>
        public void DeleteMessage(int msgID)
        {
            var msg = (from m in _context.Tbmessages where (m.MessageId == msgID) select m).First();
            //delete Tbmessage
            _context.Remove(msg);
            //save changes to context
            _context.SaveChanges();
        }

        /// <summary>
        /// Search for messages
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public List<SearchMessages> SearchMessages(int memberID, string searchKey)
        {
            List<SearchMessages> msgList = (from msg in _context.Tbmessages
                                            join mpf in _context.Tbmemberprofiles on msg.SenderId equals mpf.MemberId
                                            where (mpf.FirstName!.Contains(searchKey) || mpf.LastName!.Contains(searchKey)
                                                   || msg.Subject!.Contains(searchKey) || msg.Body!.Contains(searchKey))
                                             && msg.ContactId == memberID
                                            orderby msg.MsgDate descending
                                            select new SearchMessages()
                                            {
                                                Attachement = msg.Attachment.ToString() ?? "",
                                                Body = (msg.Body!.Length > 35) ? msg.Body.Substring(0, Math.Min(msg.Body.Length, 33)) + "..." : msg.Body,
                                                ContactName = mpf.FirstName + " " + mpf.LastName,
                                                ContactImage = Getpicturepath(Convert.ToInt32(msg.ContactId), _context),//string.IsNullOrEmpty((from p in _context.TbMemberProfiles where p.MemberId == msg.ContactId select p.MemberId).ToList()[0].ToString()) ? "default.png" : (from p in _context.TbMemberProfiles where p.MemberId == msg.ContactId select p.MemberId).ToList()[0].ToString(),
                                                SenderImage = string.IsNullOrEmpty(mpf.PicturePath) ? "default.png" : mpf.PicturePath,// mpf.PicturePath,
                                                ContactID = memberID.ToString(),
                                                FlagLevel = msg.FlagLevel.ToString() ?? "",
                                                ImportanceLevel = msg.ImportanceLevel.ToString() ?? "",
                                                MessageID = msg.MessageId.ToString(),
                                                MessageState = msg.MessageState.ToString(),
                                                SenderID = mpf.FirstName + " " + mpf.LastName,
                                                Subject = msg.Subject ?? "",
                                                MsgDate = msg.MsgDate.ToString() ?? "",
                                                FromID = msg.SenderId.ToString() ?? "",
                                                FirstName = mpf.FirstName ?? "",
                                                FullBody = msg.Body

                                            }).ToList();
            return msgList;
        }

        /// <summary>
        /// Empty messages folder
        /// </summary>
        /// <param name="mID"></param>
        public void EmptyMessageFolders(int mID)
        {
            var msg = (from m in _context.Tbmessages where m.ContactId == mID select m).First();
            _context.Remove(msg);
            _context.SaveChanges();
        }

        /// <summary>
        /// Helper to get picture image file name for member
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private static string Getpicturepath(int id, DBContext context)
        {
            var q = (from p in context.Tbmemberprofiles where p.MemberId == id select p.PicturePath).ToList();
            if (q == null)
            {
                return "default.png";
            }
            else
            {
                return q[0].ToString();
            }
        }
    }
}