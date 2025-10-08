using System.Net;
using System.Net.Mail;

namespace dotnet_sp_api.Helpers
{
    public static class SendMailHelper
    {
        /// <summary>
        ///  Send email given recipient info using smtp (gmail server) as define in AppString properties in Appsettings.json
        /// </summary>
        /// <param name="smtpHost"></param>
        /// <param name="smtpPort"></param>
        /// <param name="appSMTPpwd"></param>
        /// <param name="name"></param>
        /// <param name="fromEmail"></param>
        /// <param name="toEmail"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        public static void SendMail(string smtpHost, int smtpPort, string appSMTPpwd, string name, string fromEmail, string toEmail, string subject, string body, bool isBodyHtml)
        {
            //(1) Create the MailMessage instance
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
            var client = new SmtpClient(smtpHost, smtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, appSMTPpwd),
                EnableSsl = true
            };

            //(4) Send the MailMessage
            client.Send(mail);
        }
    }
}

