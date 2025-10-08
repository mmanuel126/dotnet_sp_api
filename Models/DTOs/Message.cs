namespace dotnet_sp_api.Models.DTOs
{
    /// <summary>
    /// Stores message info. 
    /// </summary>
    public class MessageInfo
    {
        public string MessageID { get; set; } = string.Empty;
        public string SentDate { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string SenderPicture { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string AttachmentFile { get; set; } = string.Empty;
        public string OrginalMsg { get; set; } = string.Empty;
    }

    /// <summary>
    /// Holds the message input.
    /// </summary>
    public class MessageInputs
    {
        public string to { get; set; } = string.Empty;
        public string from { get; set; } = string.Empty;
        public string subject { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
        public string attachement { get; set; } = string.Empty;
        public string original_msg { get; set; } = string.Empty;
        public string message_id { get; set; } = string.Empty;
        public string sent_date { get; set; } = string.Empty;
        public string sender_picture { get; set; } = string.Empty;
    }

    /// <summary>
    /// Stores searched messages result info 
    /// </summary>
    public class SearchMessages
    {
        public string Attachement { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string ContactImage { get; set; } = string.Empty;
        public string SenderImage { get; set; } = string.Empty;
        public string ContactID { get; set; } = string.Empty;
        public string FlagLevel { get; set; } = string.Empty;
        public string ImportanceLevel { get; set; } = string.Empty;
        public string MessageID { get; set; } = string.Empty;
        public string MessageState { get; set; } = string.Empty;
        public string SenderID { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string MsgDate { get; set; } = string.Empty;
        public string FromID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string FullBody { get; set; } = string.Empty;
        public string SenderTitle { get; set; } = string.Empty;
    }
}