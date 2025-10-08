using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbforgotpwdcode
{
    public long CodeId { get; set; }

    public string? Email { get; set; }

    public DateTime? Codedate { get; set; }

    public long? Status { get; set; }
}
