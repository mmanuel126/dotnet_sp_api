using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbad
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Headertext { get; set; }

    public DateTime? Postingdate { get; set; }

    public string? Textfield { get; set; }

    public string? Navigateurl { get; set; }

    public string? Imageurl { get; set; }

    public string? Type { get; set; }
}
