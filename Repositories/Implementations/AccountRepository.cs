using dotnet_sp_api.Models.DBContextModels;
using Microsoft.EntityFrameworkCore;
using dotnet_sp_api.Models.DTOs;
using dotnet_sp_api.Helpers;
using dotnet_sp_api.Repositories.Interfaces;

namespace dotnet_sp_api.Repositories.Implementations
{
    /// <summary>
    /// Describes the functionalities for accessing data for security
    /// </summary>
    public class AccountRepository(DBContext context, IConfiguration configuration, Crypto crypto, ICommonRepository commonRepo) : IAccountRepository
    {
        readonly DBContext _context = context;
        private readonly IConfiguration _configuration = configuration;
        private readonly Crypto _encryptor = crypto;
        private readonly ICommonRepository _commonRepo = commonRepo;

        /// <summary>
        /// Allows you to change user password.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPwd"></param>
        public void ChangePassword(string email, string newPwd)
        {
            var sql = "SELECT sp_change_password_via_email(@email, @new_pwd)";
            _context.Database.ExecuteSqlRaw(sql,
                new Npgsql.NpgsqlParameter("@email", email),
                new Npgsql.NpgsqlParameter("@new_pwd", newPwd));
        }

        /// <summary>
        /// validates a user to see if he/she has an account on this site.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>User</returns>
        public List<User> ValidateUser(Login data)
        {
            string strPwd = _encryptor.Encrypt(data.Password);

            var mlist = (from c in _context.Tbmembers
                         join m in _context.Tbmemberprofiles on c.MemberId equals m.MemberId
                         where (c.Email == data.Email) && (c.Password == strPwd) && (c.Status == 2 || c.Status == 3)
                         select new User()
                         {
                             MemberID = c.MemberId.ToString(),
                             Name = m.FirstName + " " + m.LastName,
                             Email = c.Email ?? "",
                             PicturePath = m.PicturePath ?? "",
                             Title = m.TitleDesc ?? "",
                             CurrentStatus = c.Status.ToString() ?? "0"
                         }).ToList();
            return mlist;
        }

        /// <summary>
        /// validates a new registered user. returns a row of info about the validated user. 
        /// </summary>
        /// <param name="strEmail"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<ValidateNewRegisteredUser> ValidateNewRegisteredUser(string strEmail, int code)
        {
            List<ValidateNewRegisteredUser> lst = (from m in _context.Tbmembers
                                                   join mp in _context.Tbmemberprofiles on m.MemberId equals mp.MemberId
                                                   join mr in _context.Tbmembersregistereds on m.MemberId equals mr.MemberId
                                                   where (mr.MemberCodeId == code) && (m.Email == strEmail) && (m.Status == 1)
                                                   select new ValidateNewRegisteredUser()
                                                   {
                                                       MemberId = m.MemberId.ToString(),
                                                       Email = m.Email ?? "",
                                                       FirstName = mp.FirstName ?? "",
                                                       LastName = mp.LastName ?? "",
                                                       PassWord = m.Password ?? "",
                                                       UserImage = mp.PicturePath ?? "",
                                                       Title = mp.TitleDesc ?? ""
                                                   }

                    ).ToList();
            if (lst.Count != 0)
            {
                var mem = (from z in _context.Tbmembers where z.MemberId == Convert.ToInt32(lst[0].MemberId) select z).First();
                mem.Status = 2;
                _context.SaveChanges();
            }
            return (lst);
        }

        public List<User> FindByUniqueEmail(string strEmail)
        {
            List<User> lst = (from m in _context.Tbmembers
                              join mp in _context.Tbmemberprofiles on m.MemberId equals mp.MemberId
                              where m.Email == strEmail
                              select new User()
                              {
                                  MemberID = m.MemberId.ToString(),
                                  Email = m.Email ?? "",
                                  PicturePath = mp.PicturePath ?? ""
                              }

                   ).ToList();
            return lst;
        }

        /// <summary>
        /// Creates the new user.
        /// </summary>
        /// <returns>The new user.</returns>
        /// <param name="body">body</param>
        public int CreateNewUser(Register body)
        {
            using var conn = new Npgsql.NpgsqlConnection(_context.Database.GetConnectionString());
            conn.Open();

            using var cmd = new Npgsql.NpgsqlCommand("SELECT public.sp_create_new_user(@FirstName, @LastName, @Email, @Password, @Gender, @Month, @Day, @Year, @ProfileType)", conn);

            cmd.Parameters.AddWithValue("@FirstName", body.FirstName);
            cmd.Parameters.AddWithValue("@LastName", body.LastName);
            cmd.Parameters.AddWithValue("@Email", body.Email);
            cmd.Parameters.AddWithValue("@Password", body.Password);
            cmd.Parameters.AddWithValue("@Gender", body.Gender);
            cmd.Parameters.AddWithValue("@Month", body.Month);
            cmd.Parameters.AddWithValue("@Day", body.Day);
            cmd.Parameters.AddWithValue("@Year", body.Year);
            cmd.Parameters.AddWithValue("@ProfileType", body.ProfileType);

            var result = cmd.ExecuteScalar(); // scalar = single value return
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// check to see if email exists -- everyone on here has a unique email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns>return list if email exist</returns>
        public List<Tbmember> CheckEmailExists(string email)
        {
            var mlist = (from m in _context.Tbmembers where (m.Email == email) select m).ToList();
            return mlist;
        }

        /// <summary>
        /// get code and name for forgot passwords
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<CodeAndNameForgotPwdModel> GetCodeAndNameForgotPwd(string email)
        {
            List<CodeAndNameForgotPwdModel> bLST = new List<CodeAndNameForgotPwdModel>();
            var l = (from m in _context.Tbmembers
                     join mp in _context.Tbmemberprofiles on m.MemberId equals mp.MemberId
                     where m.Email == email
                     select mp).ToList();

            var memID = l[0].MemberId;
            var fName = l[0].FirstName;

            CodeAndNameForgotPwdModel lst = new CodeAndNameForgotPwdModel();
            if (memID != 0)
            {
                dotnet_sp_api.Models.DBContextModels.Tbforgotpwdcode ins = new()
                {
                    Email = email,
                    Codedate = System.DateTime.Now,
                    Status = 0
                };

                _context.Tbforgotpwdcodes.Add(ins);
                _context.SaveChanges();

                lst.Code = ins.CodeId.ToString();
                lst.Email = fName ?? "";
                bLST.Add(lst);
            }
            else
            {
                lst.Code = "0";
                lst.Email = "";
                bLST.Add(lst);
            }
            return bLST;
        }

        /// <summary>
        /// checked code expired
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<Tbforgotpwdcode> CheckCodeExpired(int code)
        {
            var flist = (from f in _context.Tbforgotpwdcodes where (f.CodeId == code && f.Status == 0) select f).ToList();
            return flist;
        }

        /// <summary>
        /// Set the code to expire 
        /// </summary>
        /// <param name="code"></param>
        public void SetCodeToExpire(int code)
        {
            var fEdit = (from f in _context.Tbforgotpwdcodes where f.CodeId == code select f).First();
            fEdit.Status = 1;
            _context.SaveChanges();
        }

        /// <summary>
        /// check to see if user is active
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public bool IsActiveUser(int memberID)
        {
            var q = (from m in _context.Tbmembers where m.MemberId == memberID && m.Status == 2 select m).ToList();
            if (q.Count != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Set status for member id - active, newregister, or inactive
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="status"></param>
        public void SetMemberStatus(int memberID, int status)
        {
            var mEdit = (from m in _context.Tbmembers where m.MemberId == memberID select m).First();
            mEdit.Status = status;
            _context.SaveChanges();
        }

        /// <summary>
        /// Authenticate the user using email and pwd
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public User AuthenticateUser(Login data)
        {
            string strEmail = data.Email;
            string strPwd = _encryptor.Encrypt(data.Password.Trim());
            List<User> memberList = ValidateUser(data);
            if (memberList.Count != 0)
                return memberList[0];
            else
                return new User();
        }

        public ValidateNewRegisteredUser AuthenticateNewRegisteredUser(string email, string code)
        {
            List<ValidateNewRegisteredUser> memberList = ValidateNewRegisteredUser(email, Convert.ToInt32(code));
            if (memberList.Count != 0)
                return memberList[0];
            else
                return new ValidateNewRegisteredUser();
        }

        public String RegisterUser(Register data)
        {
            List<Tbmember> lst = CheckEmailExists(data.Email);
            if (lst.Count != 0)
            {
                return "ExistingEmail";
            }
            else
            {
                data.Password = _encryptor.Encrypt(data.Password.Trim());
                int code = CreateNewUser(data);
                SendEmail(data.Email, code.ToString(), data.FirstName, data.LastName);
                return "NewEmail";
            }
        }

        private void SendEmail(string email, string code, string firstName, string lastName)
        {
            string fromEmail = _configuration.GetValue<string>("AppStrings:AppFromEmail") ?? "";
            string toEmail = email;
            string fullName = firstName.Trim() + " " + lastName.Trim();
            string subject = "Account Confirmation";
            string body = HTMLEmailBodyText(email, fullName, code, firstName);
            _commonRepo.SendMail("", fromEmail, toEmail, subject, body, true);
        }

        private string HTMLEmailBodyText(string email, string name, string code, string firstName)
        {
            string appName = _configuration.GetValue<string>("AppStrings:AppName") ?? "";
            string webSiteLink = _configuration.GetValue<string>("AppStrings:CompleteRegistrationLink") ?? "";
            string str = "";

            str = str + "<table width='100%' style='text-align: center;'>";
            str = str + "<tr>";
            str = str + "<td style='font-weight: bold; font-size: 12px; height: 25px; text-align: left; background-color: red";
            str = str + "vertical-align: middle; color: White;'>";
            str = str + "&nbsp;" + appName;
            str = str + "</td>";
            str = str + "</tr>";
            str = str + "<tr><td>&nbsp;</td></tr>";
            str = str + "<tr>";
            str = str + "<td style='font-size: 12px; text-align: left; width: 100%; font-family: Trebuchet MS,Trebuchet,Verdana,Helvetica,Arial,sans-seri'>";
            str = str + "<p>Hi " + name + ",<p/>";
            str = str + "</td>";
            str = str + "</tr>";
            str = str + "<tr>";
            str = str + "<td style='font-size: 12px; text-align: left; width: 100%; font-family: Trebuchet MS,Trebuchet,Verdana,Helvetica,Arial,sans-seri'>";
            str = str + "<p>You recently registered for " + appName + ". To complete your registration, click the link below (or copy/paste the link to a browser):<br/>";
            str = str + "<p />";
            str = str + "<p><a href ='" + webSiteLink + "?code=" + code.ToString() + "&email=" + email + "&fname=" + firstName + "'>" + webSiteLink + "?code=" + code.ToString() + "&email=" + email + "</a></p>";
            str = str + "<p/>";
            str = str + "<p>Your registration code is: " + code;
            str = str + "<p/>";
            str = str + "<p>" + appName + " is an exciting new sport social networking site that helps athletes showcase their talents so they can potentially attract sport agents. It is also a tool for people to communicate and stay connected with other sport fanatics. Once you become ";
            str = str + "a member, you'll be able to share your sport experience with the rest of the world.</p>";
            str = str + "<p/>";
            str = str + "Thanks.<br />";
            str = str + appName + " Team<br />";
            str = str + "</p>";
            str = str + "<p></p><p>";
            str = str + "</td>";
            str = str + "</tr>";
            str = str + "</table>";
            return str;
        }

    }

}

