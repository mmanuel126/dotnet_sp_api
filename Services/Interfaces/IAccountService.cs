using dotnet_sp_api.Models.DTOs;
using dotnet_sp_api.Models.DBContextModels;

namespace dotnet_sp_api.Interfaces
{
   public interface IAccountService
   {
      List<CodeAndNameForgotPwdModel> GetCodeAndNameForgotPwd(string email);
      string CheckCodeExpired(int code);
      void SetCodeToExpire(int code);
      string ChangePassword(string pwd, string email, string code);
      List<User> ValidateUser(Login data);
      List<Tbmember> CheckEmailExists(string email);
      List<User> FindByUniqueEmail(string strEmail);
      string RegisterUser(Register data);
      User AuthenticateUser(Login data);
      User AuthenticateNewRegisteredUser(string email, string code);
      string RefreshToken(string accessToken);
      string ResetPassword(string email);
      void SetMemberStatus(int memberID, int status);
   }
}