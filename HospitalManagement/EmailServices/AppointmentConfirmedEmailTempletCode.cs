using System.Net.Mail;
using System.Net;

namespace HospitalManagement.EmailServices
{

    public class AppointmentConfirmedEmailTempletCode
    {
        private readonly IConfiguration _configuration;


        public AppointmentConfirmedEmailTempletCode(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public bool AppointmentConfirmedEmailTempletCodeSend(string Recipient, string MailBody)
        {
            string GmailAccountEmail = _configuration["EmailSettings:Email"];
            string GmailAccountPassword = _configuration["EmailSettings:Password"];
            string SmtpServerAddress = _configuration["EmailSettings:SmtpServer"];
            string SmtpServerPort = _configuration["EmailSettings:SmtpPort"];

            try
            {
                NetworkCredential LoginInfo = new NetworkCredential(GmailAccountEmail, GmailAccountPassword);

                MailMessage message = new MailMessage();
                message.From = new MailAddress(GmailAccountEmail, "Appointment Confirmation");
                message.To.Add(new MailAddress(Recipient));
                message.Subject = "Appointment Confirmation";
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
