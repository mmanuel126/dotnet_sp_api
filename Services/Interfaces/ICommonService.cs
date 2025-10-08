using dotnet_sp_api.Models.DBContextModels;
using dotnet_sp_api.Models.DTOs;


namespace dotnet_sp_api.Services.Interfaces
{
    public interface ICommonService
    {
        List<Tbrecentnews> GetRecentNews();
        List<Tbstate> GetStates();
        List<Tbsport> GetSportsList();
        List<SchoolByState> GetSchoolsByState(string state, string institutionType);
        List<Ads> GetAds(string type);
        string Logs(string message, string stack);
        string SendAdvertisementInfo(string firstName, string lastName, string company, string email, string phone, string country, string title);
    }
}