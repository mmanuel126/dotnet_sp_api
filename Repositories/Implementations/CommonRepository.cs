using dotnet_sp_api.Models.DTOs;
using System.Data;
using dotnet_sp_api.Models.DBContextModels;
using dotnet_sp_api.Repositories.Interfaces;
using System.Net.Mail;
using System.Net;

namespace dotnet_sp_api.Repositories.Implementations
{
    /// <summary>
    /// describes the functionalities to manage the business and data requirements for Site Common usage needs
    /// </summary>
    public class CommonRepository(DBContext context, IConfiguration configuration) : ICommonRepository
    {
        public readonly DBContext _context = context;
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Get Recent news 
        /// </summary>
        /// <returns></returns>
        public List<Tbrecentnews> GetRecentNews()
        {
            var q = (from r in _context.Tbrecentnews orderby r.PostingDate descending select r).Take(8).ToList();
            return q;
        }

        /// <summary>
        /// Get states listing
        /// </summary>
        /// <returns></returns>
        public List<Tbstate> GetStates()
        {
            var q = (from s in _context.Tbstates orderby s.Name ascending select s).ToList();
            return q;
        }

        /// <summary>
        /// Get the list of all sports
        /// </summary>
        /// <returns></returns>
        public List<Tbsport> GetSportsList()
        {

            return _context.Tbsports.OrderBy(s => s.Name).ToList();
        }

        /// <summary>
        /// Get schools by state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="institutionType"></param>
        /// <returns></returns>
        public List<SchoolByState> GetSchoolsByState(string state, string institutionType)
        {
            state = state.ToUpper();

            List<SchoolByState> sM = new();
            if (institutionType == "1") //public
            {
                sM = (from s in _context.Tbpublicschools
                      where s.State == state
                      orderby s.SchoolName ascending

                      select new SchoolByState()
                      {
                          SchoolId = s.Lgid.ToString(),
                          SchoolName = s.SchoolName ?? ""
                      }
                        ).Distinct().ToList();
            }
            else if (institutionType == "2") //private
            {
                sM = (from s in _context.Tbprivateschools
                      where s.State == state
                      orderby s.SchoolName ascending

                      select new SchoolByState()
                      {
                          SchoolId = s.LgId.ToString(),
                          SchoolName = s.SchoolName ?? ""
                      }
                        ).Distinct().ToList();
            }
            else if (institutionType == "3") //colleges
            {
                sM = (from s in _context.Tbcolleges
                      where s.State == state
                      orderby s.Name ascending

                      select new SchoolByState()
                      {
                          SchoolId = s.SchoolId.ToString(),
                          SchoolName = s.Name ?? ""
                      }
                        ).Distinct().ToList();
            }
            return sM;
        }

        /// <summary>
        /// Returns the list of ads for the site.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Ads> GetAds(string type)
        {
            return _context.Tbads
                .Where(a => a.Type == type)
                .Select(a => new Ads
                {
                    ID = (int)a.Id,
                    Name = a.Name!,
                    HeaderText = a.Headertext!,
                    PostingDate = (DateTime)a.Postingdate!,
                    TextField = a.Textfield!,
                    NavigateUrl = a.Navigateurl!,
                    ImageUrl = a.Imageurl!,
                    Type = a.Type!
                })
                .ToList();
        }

        /// <summary>
        /// Send email given recipient info.
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="fromEmail"></param>
        /// <param name="toEmail"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        public void SendMail(string memberName, string fromEmail, string toEmail, string subject, string body, bool isBodyHtml)
        {
            string name = memberName;

            if (string.IsNullOrEmpty(name))
                name = _configuration.GetValue<string>("AppStrings:AppAdmin") ?? "";

            //(1) Create the MailMessage instance
            fromEmail = _configuration.GetValue<string>("AppStrings:AppFromEmail") ?? "";

            MailMessage mail = new()
            {
                From = new MailAddress(fromEmail, name) //IMPORTANT: This must be same as your smtp authentication address.
            };
            mail.To.Add(toEmail);

            //(2) Assign the MailMessage's properties
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            //(3) Create the SmtpClient object
            string smtpHost = _configuration.GetValue<string>("AppStrings:AppSMTPHost") ?? "";

            int smtpPort = 587;
            if (String.IsNullOrEmpty(_configuration.GetValue<string>("AppStrings:AppSMTPPort")))
            {
                smtpPort = Convert.ToInt32(_configuration.GetValue<string>("AppStrings:AppSMTPPort"));
            }

            string appSMTPpwd = _configuration.GetValue<string>("AppStrings:AppSMTPpwd") ?? "";

            var client = new SmtpClient(smtpHost, smtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, appSMTPpwd),
                EnableSsl = true
            };
            client.Send(mail);
        }
    }
}
