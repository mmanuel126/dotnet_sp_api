using System;
using System.Collections.Generic;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class Tbcollege
{
    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Website { get; set; }

    public string? Type { get; set; }

    public string? AwardsOffered { get; set; }

    public string? CampusSetting { get; set; }

    public string? CampusHousing { get; set; }

    public long? StudentPopulation { get; set; }

    public long? UndergradStudents { get; set; }

    public string? StudentToFacultyRatio { get; set; }

    public string? Ipedsid { get; set; }

    public string? Opeid { get; set; }

    public string? State { get; set; }

    public long SchoolId { get; set; }

    public string? Imagefile { get; set; }
}
