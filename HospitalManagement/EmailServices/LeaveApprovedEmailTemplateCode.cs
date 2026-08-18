using System.Net;
using System.Net.Mail;

namespace HospitalManagement.EmailServices
{
    public class LeaveApprovedEmailTemplateCode
    {

        private readonly IConfiguration _Configuration;

        public LeaveApprovedEmailTemplateCode(IConfiguration _Configuration)
        {
            this._Configuration = _Configuration;
        }


        public bool LeaveApprovedEmailTemplateCodeSend(string Recipient, string MailBody)
        {
            string GmailAccountEmail = _Configuration["EmailSettings:Email"];
            string GmailAccountPassword = _Configuration["EmailSettings:Password"];
            string SmtpServerAddress = _Configuration["EmailSettings:SmtpServer"];
            string SmtpServerPort = _Configuration["EmailSettings:SmtpPort"];
            try
            {
                NetworkCredential LoginInfo = new NetworkCredential(GmailAccountEmail, GmailAccountPassword);
                MailMessage message = new MailMessage();
                message.From = new MailAddress(GmailAccountEmail, "Leave Approved");
                message.To.Add(new MailAddress(Recipient));
                message.Subject = "Leave Approved";
                message.Body = MailBody;
                message.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient(SmtpServerAddress, Convert.ToInt32(SmtpServerPort));
                smtpClient.Credentials = LoginInfo;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Send(message);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
