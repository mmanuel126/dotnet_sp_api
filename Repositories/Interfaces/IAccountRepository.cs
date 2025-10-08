
using dotnet_sp_api.Models.DBContextModels;
using dotnet_sp_api.Models.DTOs;

namespace dotnet_sp_api.Repositories.Interfaces
{
   public interface IAccountRepository
   {
      List<ValidateNewRegisteredUser> ValidateNewRegisteredUser(string strEmail, int code);
      List<CodeAndNameForgotPwdModel> GetCodeAndNameForgotPwd(string email);
      List<Tbforgotpwdcode> CheckCodeExpired(int code);
      void SetCodeToExpire(int code);
      void ChangePassword(string email, string newPwd);
      List<User> ValidateUser(Login data);
      List<Tbmember> CheckEmailExists(string email);
      List<User> FindByUniqueEmail(string strEmail);
      string RegisterUser(Register data);
      void SetMemberStatus(int memberID, int status);
      User AuthenticateUser(Login data);
      ValidateNewRegisteredUser AuthenticateNewRegisteredUser(string email, string code);
   }
}