using dotnet_sp_api.Services.Interfaces;
using dotnet_sp_api.Repositories.Interfaces;
using dotnet_sp_api.Models.DBContextModels;
using dotnet_sp_api.Models.DTOs;
using dotnet_sp_api.Helpers;

namespace dotnet_sp_api.Services.Implementations
{
    /// <summary>
    /// Describes the functionalities for common service
    /// </summary>
    public class CommonService(ICommonRepository commonRepository, ILoggerFactory loggerFactory, IConfiguration configuration) : ICommonService
    {
        private readonly ICommonRepository _commonRepository = commonRepository;
        private readonly IConfiguration _configuration = configuration;
        readonly ILogger _loggerFactory = loggerFactory.CreateLogger("SP_APP");

        public List<Tbstate> GetStates()
        {
            return _commonRepository.GetStates();
        }

        /// <summary>
        /// Logs the specified obj log model to file including error msg and stack trace.
        /// </summary>
        /// <param name="message">the error message to log.</param>
        /// <param name="stack">the errorstack to log.</param>
        public string Logs(string message, string stack)
        {
            string logText = LogText(message, stack);
            _loggerFactory.LogError(logText);
            return "success";
        }

        /// <summary>
        /// returns sports lists.
        /// </summary>
        /// <returns></returns>
        public List<Tbsport> GetSportsList()
        {
            return _commonRepository.GetSportsList();
        }

        /// <summary>
        /// Gets school by state.
        /// </summary>
        /// <returns>The school by state.</returns>
        /// <param name="state">State.</param>
        /// <param name="institutionType">Institution type.</param>
        public List<SchoolByState> GetSchoolsByState(string state, string institutionType)
        {
            return _commonRepository.GetSchoolsByState(state, institutionType);
        }

        /// <summary>
        /// returns a list of ads depending on type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Ads> GetAds(string type)
        {
            return _commonRepository.GetAds(type);
        }

        /// <summary>
        /// Logs the text.
        /// </summary>
        /// <returns>The text.</returns>
        /// <param name="msg">Message.</param>
        /// <param name="stack">Stack.</param>
        private string LogText(string msg, string stack)
        {
            string txt = "";
            txt += "\n";
            txt = txt + "Message: " + msg + "\n";
            txt = txt + "Stack Trace: " + stack + "\n";
            txt += "\n";
            return txt;
        }

        /// <summary>
        /// Get the recent news.
        /// </summary>
        /// <returns>The recent news.</returns>
        public List<Tbrecentnews> GetRecentNews()
        {
            return _commonRepository.GetRecentNews();
        }

        /// <summary>
        /// Sends the advertisement info.
        /// </summary>
        /// <returns>The advertisement info.</returns>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="company">Company.</param>
        /// <param name="email">Email.</param>
        /// <param name="phone">Phone.</param>
        /// <param name="country">Country.</param>
        /// <param name="title">Title.</param>
        public string SendAdvertisementInfo(string firstName,
                                         string lastName,
                                          string company,
                                          string email,
                                          string phone,
                                         string country,
                                          string title)
        {
            string staffContactEmail = _configuration.GetValue<string>("AppStrings:StaffContactEmail") ?? "";
            string noReplyEmail = _configuration.GetValue<string>("AppStrings:NoReplyEmail") ?? "";

            string issue = company + " wants to do business with you as far as advertisement.";
            string txtDesc = firstName + " " + lastName + " from company " + company + " wants has sent you the following information with business interests: <br/>";
            txtDesc += " - Phone: " + phone + "<br/> - Email: " + email + "<br/> - Country: " + country + "<br/> - Title: " + title;
            string strHTML = HTMLContactUsBodyText("System Administrator", email, "Advertisement Interest", issue, txtDesc);

            SendGenEmailToUser(firstName, noReplyEmail, staffContactEmail, "", "", "Advertisement Interest" + " (" + issue + ")", strHTML);

            return "success";
        }


        /// <summary>
        /// Send genearl email to user.
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="fromEmail"></param>
        /// <param name="toEmail"></param>
        /// <param name="fromName"></param>
        /// <param name="toName"></param>
        /// <param name="subject"></param>
        /// <param name="msg"></param>
        public void SendGenEmailToUser(string memberName, string fromEmail, string toEmail, string fromName, string toName, string subject, string msg)
        {
            string name = memberName;

            if (string.IsNullOrEmpty(memberName))
                name = _configuration.GetValue<string>("AppStrings:AppAdmin") ?? "";

            //(1) Create the MailMessage instance
            fromEmail = _configuration.GetValue<string>("AppStrings:AppFromEmail") ?? "";
            string appName = _configuration.GetValue<string>("AppStrings:AppName") ?? "";
            string webSiteLink = _configuration.GetValue<string>("AppStrings:WebSiteLink") ?? "";
            string pwd = _configuration.GetValue<string>("AppStrings:AppSMTPpwd") ?? "";
            string body = HTMLBodyText(fromName, toName, msg, appName, webSiteLink) ?? "";

            //(3) Create the SmtpClient object
            string smtpHost = _configuration.GetValue<string>("AppStrings:AppSMTPHost") ?? "";
            int port = Convert.ToInt32(_configuration.GetValue<string>("AppStrings:AppSMTPPort") ?? "587");

            //(4) Send the MailMessage
            SendMailHelper.SendMail(smtpHost, port, pwd, name, fromEmail, toEmail, subject, body, true);
        }


        private string HTMLContactUsBodyText(string name, string strUserEmail, string subject, string issueType, string desc)
        {
            string appName = _configuration.GetValue<string>("AppStrings:AppName") ?? "";
            string str = "";
            str = str + "<table width='100%' style='text-align: center;'>";
            str = str + "<tr>";
            str = str + "<td style='font-weight: bold; font-size: 12px; height: 25px; text-align: left; background-color: #4a6792;";
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
            str = str + "<p>On behalf of " + appName + " user " + strUserEmail + ", the following message from ContactUs page was sent:" + "<br/>";
            str = str + "<p />";
            str = str + "<p><b>Email Address:</b> " + strUserEmail + " <br/>";
            str = str + "<b>Subject:</b> " + subject + " <br/>";
            str = str + "<b>Issue type:</b> " + issueType + " <br/>";
            str = str + "<b>Description:</b> " + desc + " <br/>";
            str = str + "<p/>";
            str = str + "Thanks.<br />";
            str = str + "The " + appName + " Staff<br />";
            str = str + "</p>";
            str = str + "<p></p><p>";
            str = str + "</td>";
            str = str + "</tr>";
            str = str + "</table>";
            return str;
        }

        /// <summary>
        /// Returns the email body formed in HTML.
        /// </summary>
        /// <param name="fromName"></param>
        /// <param name="toName"></param>
        /// <param name="msg"></param>
        /// <param name="appName"></param>
        /// <param name="webSiteLink"></param>
        /// <returns></returns>
        private static string HTMLBodyText(string fromName, string toName, string msg, string appName, string webSiteLink)
        {
            string str = "";

            str += "<table width='100%' style='text-align: center;'>";
            str += "<tr>";
            str += "<td style='font-weight: bold; font-size: 14pt; height: 25px; text-align: left; background-color: #4a6792;";
            str += "vertical-align: middle; color: White; font-family:Lucida Grande, tahoma, helvetica;'>";
            str = str + "&nbsp;" + appName;
            str += "</td>";
            str += "</tr>";
            str += "<tr><td>&nbsp;</td></tr>";
            str += "<tr>";
            str += "<td style='font-size: 12pt; text-align: left; width: 100%; font-family: font-family:Lucida Grande, tahoma, helvetica;'>";
            str = str + "<p>Hi " + toName + ",<p/>";
            str += "</td>";
            str += "</tr>";
            str += "<tr>";
            str += "<td style='font-size: 12pt; text-align: left; width: 100%; font-family: font-family:Lucida Grande, tahoma, helvetica;'>";
            str = str + "<p>" + fromName + " has sent you the following message from " + appName + ":<br/><br/>";
            str += "<p></p><p>";
            str = str + "<p> " + msg + "<br/>";
            str += "</p>";

            str += "<p></p><p>";

            str += "You can access the site via the link below. <br />";
            str = str + "<a href='" + webSiteLink + "'>" + webSiteLink + "</a></p>";

            str += "<p>";
            str += "Sincerely yours,<br />";
            str = str + appName + " Team <br />";
            str += "</p>";
            str += "</td>";
            str += "</tr>";
            str += "</table>";

            return str;
        }
    }
}