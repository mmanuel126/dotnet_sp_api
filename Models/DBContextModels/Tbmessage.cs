using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbmessage
{
    public long MessageId { get; set; }

    public long? SenderId { get; set; }

    public long? ContactId { get; set; }

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public DateTime? MsgDate { get; set; }

    public decimal? Attachment { get; set; }

    public long MessageState { get; set; }

    public long? FlagLevel { get; set; }

    public long? ImportanceLevel { get; set; }

    public string? AttachmentFile { get; set; }

    public string? Source { get; set; }

    public string? OriginalMsg { get; set; }

    public virtual Tbmember? Contact { get; set; }

    public virtual Tbmember? Sender { get; set; }
}
