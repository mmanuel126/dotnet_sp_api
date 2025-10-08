using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbmemberprofilepersonalinfo
{
    public long MemberId { get; set; }

    public string? Activities { get; set; }

    public string? Interests { get; set; }

    public string? FavoriteMusic { get; set; }

    public string? FavoriteTvShows { get; set; }

    public string? FavoriteMovies { get; set; }

    public string? FavoriteBooks { get; set; }

    public string? FavoriteQuotations { get; set; }

    public string? AboutMe { get; set; }

    public string? SpecialSkills { get; set; }

    public virtual Tbmember Member { get; set; } = null!;
}
