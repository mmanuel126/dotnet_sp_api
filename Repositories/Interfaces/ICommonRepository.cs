using dotnet_sp_api.Models.DBContextModels;
using dotnet_sp_api.Models.DTOs;

namespace dotnet_sp_api.Repositories.Interfaces
{
    public interface ICommonRepository
    {
        List<Tbrecentnews> GetRecentNews();
        List<Tbstate> GetStates();
        List<Tbsport> GetSportsList();
        List<SchoolByState> GetSchoolsByState(string state, string institutionType);
        List<Ads> GetAds(string type);
        void SendMail(string memberName, string fromEmail, string toEmail, string subject, string body, bool isBodyHtml);
    }
}