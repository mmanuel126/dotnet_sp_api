using dotnet_sp_api.Models.DTOs;
using dotnet_sp_api.Interfaces;
using dotnet_sp_api.Models.DBContextModels;
using dotnet_sp_api.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using dotnet_sp_api.Repositories.Interfaces;

namespace dotnet_sp_api.Services.Implementations
{
    /// <summary>
    /// Describes the functionalities for accessing account data for security
    /// </summary>
    public class AccountService(IAccountRepository accountRepository, IConfiguration configuration, Crypto crypto) : IAccountService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly IConfiguration _configuration = configuration;
        private readonly Crypto _encryptor = crypto;

        /// <summary>
        /// Allows you to change user password.
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public string ChangePassword(string pwd, string email, string code)
        {
            pwd = _encryptor.Encrypt(pwd);
            if (code != "")
            {
                _accountRepository.SetCodeToExpire(Convert.ToInt32(code));
            }
            _accountRepository.ChangePassword(email, pwd);

            var data = new Login
            {
                Email = email,
                Password = pwd
            };
            var memberList = _accountRepository.ValidateUser(data);
            if (memberList.Count != 0)
            {
                return memberList[0].MemberID.ToString() + ":" + memberList[0].Email.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// validates a user to see if he/she has an account on this site.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<User> ValidateUser(Login data)
        {
            return _accountRepository.ValidateUser(data);
        }

        /// <summary>
        /// validates a new registered user. returns a row of info about the validated user. 
        /// </summary>
        /// <param name="strEmail"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<ValidateNewRegisteredUser> ValidateNewRegisteredUser(string strEmail, int code)
        {
            return _accountRepository.ValidateNewRegisteredUser(strEmail, code);
        }

        /// <summary>
        /// find by unique email.
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        public List<User> FindByUniqueEmail(string strEmail)
        {
            return _accountRepository.FindByUniqueEmail(strEmail);
        }

        /// <summary>
        /// Creates the new user.
        /// </summary>
        /// <returns>The new user.</returns>
        /// <param name="body">body</param>
        public string RegisterUser(Register body)
        {
            return _accountRepository.RegisterUser(body);
        }

        /// <summary>
        /// check to see if email exists -- everyone on here has a unique email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns>return list if email exist</returns>
        public List<Tbmember> CheckEmailExists(string email)
        {
            return _accountRepository.CheckEmailExists(email);
        }

        /// <summary>
        /// get code and name for forgot passwords
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<CodeAndNameForgotPwdModel> GetCodeAndNameForgotPwd(string email)
        {
            return _accountRepository.GetCodeAndNameForgotPwd(email);
        }

        /// <summary>
        /// checked code expired
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string CheckCodeExpired(int code)
        {
            List<Tbforgotpwdcode> lst = _accountRepository.CheckCodeExpired(code);
            if (lst.Count == 0)
            {
                return "yes";
            }
            else
            {
                return "no";
            }
        }

        /// <summary>
        /// Set the code to expire 
        /// </summary>
        /// <param name="code"></param>
        public void SetCodeToExpire(int code)
        {
            _accountRepository.SetCodeToExpire(code);
        }

        /// <summary>
        /// Authenticate the user using email and pwd
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public User AuthenticateUser(Login data)
        {
            User user = new();

            var result = _accountRepository.AuthenticateUser(data);
            if (result != null)
            {
                user.Email = result.Email;
                user.MemberID = result.MemberID;
                user.PicturePath = result.PicturePath;
                var jwt = GetToken(ref user, out DateTime expiredDate);
                user.AccessToken = jwt;
                user.ExpiredDate = expiredDate;
                user.Name = result.Name;
                user.Title = result.Title;
                user.CurrentStatus = result.CurrentStatus;
            }
            return user;
        }

        /// <summary>
        /// Authenticate the newly created member using generated code emailed to user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public User AuthenticateNewRegisteredUser(string email, string code)
        {
            User user = new();
            var result = _accountRepository.AuthenticateNewRegisteredUser(email, code);
            if (result != null)
            {
                user.Email = result.Email;
                user.MemberID = result.MemberId;
                user.PicturePath = result.UserImage;
                var jwt = GetToken(ref user, out DateTime expiredDate);
                user.AccessToken = jwt;
                user.ExpiredDate = expiredDate;
                user.Name = result.FirstName + " " + result.LastName;
                user.Title = result.Title;
                user.CurrentStatus = "";
            }
            return user;
        }

        /// <summary>
        /// Get new access token given an existing one if valid.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string RefreshToken(string accessToken)
        {
            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity!.Name; //this is mapped to the Name claim by default

            var jwt = GetRefreshToken(out DateTime expiredDate);
            return jwt;
        }

        /// <summary>
        /// Get principal from expired token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<String>("Jwt:Key") ?? "extremlyExtremlyveryVerySecretKey")),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (principal.Identity!.IsAuthenticated == false)
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        /// <summary>
        /// Resets password.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string ResetPassword(string email)
        {
            List<CodeAndNameForgotPwdModel> lst = _accountRepository.GetCodeAndNameForgotPwd(email);
            if (lst.Count != 0)
            {
                CodeAndNameForgotPwdModel ds = lst[0];
                string code = ds.Code.ToString();
                string firstName = ds.Email.ToString();
                SendEmail(email, code, firstName);
                return "success";
            }
            else
            {
                return "fail";
            }
        }

        /// <summary>
        /// Sets member status
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="status"></param>
        public void SetMemberStatus(int memberID, int status)
        {
            _accountRepository.SetMemberStatus(memberID, status);
        }


        #region helper routines...

        private string GetToken(ref User user, out DateTime ExpiredDate)
        {
            var _key = _configuration["Jwt:Key"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key ?? "extremlyExtremlyveryVerySecretKey");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddSeconds(186000),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            ExpiredDate = token.ValidTo;
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        private string GetRefreshToken(out DateTime ExpiredDate)
        {
            var _key = _configuration["Jwt:Key"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key ?? "extremlyExtremlyveryVerySecretKey");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddSeconds(186000),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            ExpiredDate = token.ValidTo;
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        private void SendEmail(string email, string code, string firstName)
        {
            string name = _configuration.GetValue<string>("AppStrings:AppAdmin") ?? "";
            string smtpHost = _configuration.GetValue<string>("AppStrings:AppSMTPHost") ?? "";
            int smtpPort = Convert.ToInt32(_configuration.GetValue<string>("AppStrings:AppSMTPPort"));
            string appSMTPpwd = _configuration.GetValue<string>("AppStrings:AppSMTPpwd") ?? "";

            string fromEmail = _configuration.GetValue<string>("AppStrings:AppFromEmail") ?? "";
            string appName = _configuration.GetValue<string>("AppStrings:AppName") ?? "";
            string subject = "Password Reset Confirmation";
            string webSiteLink = _configuration.GetValue<string>("AppStrings:WebSiteLink") ?? "";
            string body = HTMLBodyText(email, firstName, code, appName, webSiteLink);

            SendMailHelper.SendMail(smtpHost, smtpPort, appSMTPpwd, name, fromEmail, email, subject, body, true);
        }

        private static string HTMLBodyText(string email, string name, string code, string appName, string webSiteLink)
        {
            string str = "";

            str += "<table width='100%' style='text-align: center;'>";
            str += "<tr>";
            str += "<td style='font-weight: bold; font-size: 12px; height: 25px; text-align: left; background-color: #4a6792;";
            str += "vertical-align: middle; color: White;'>";
            str = str + "&nbsp;" + appName;
            str += "</td>";
            str += "</tr>";
            str += "<tr><td>&nbsp;</td></tr>";
            str += "<tr>";
            str += "<td style='font-size: 12px; text-align: left; width: 100%; font-family: Trebuchet MS,Trebuchet,Verdana,Helvetica,Arial,sans-seri'>";
            str = str + "<p>Hi " + name + ",<p/>";
            str += "</td>";
            str += "</tr>";
            str += "<tr>";
            str += "<td style='font-size: 12px; text-align: left; width: 100%; font-family: Trebuchet MS,Trebuchet,Verdana,Helvetica,Arial,sans-seri'>";
            str += "<p>You recently requested a new password. <br/>";
            str += "<p />";
            str += "<p>Here is your reset code, which you can enter on the password reset page:<br/><b>";
            str = str + code + "</b><p />";
            str += "<p>Do not share this code. We will never call or text you for it.<br/></p>";
            str += "<p>If you did not request to reset your password, please disregard this message.<br>";
            str += "<p/>";
            str += "Thanks.<br />";
            str = str + "The " + appName + " staff<br />";
            str += "</p>";
            str += "<p></p><p>";
            str += "</td>";
            str += "</tr>";
            str += "</table>";

            return str;
        }

        #endregion

    }
}

