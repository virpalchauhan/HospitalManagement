using System.Net;
using System.Net.Mail;

namespace HospitalManagement.EmailServices
{
    public class RegistrationOTPVerificationCode
    {

        private readonly IConfiguration _configuration;

        public RegistrationOTPVerificationCode(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        public  bool RegistrationOTPVerificationCodeSend(string Recipient, string MailBody)
        {
            try
            {
                string GmailAccountEmail = _configuration["EmailSettings:Email"];
                string GmailAccountPassword = _configuration["EmailSettings:Password"];
                string SmtpServerAddress = _configuration["EmailSettings:SmtpServer"];
                string SmtpServerPort = _configuration["EmailSettings:SmtpPort"];

                NetworkCredential LoginInfo = new NetworkCredential(GmailAccountEmail, GmailAccountPassword);
                MailMessage Message = new MailMessage();
                Message.From = new MailAddress(GmailAccountEmail, "Registration OTP Verification Code");
                Message.To.Add(new MailAddress(Recipient));
                Message.Subject = "Registration OTP Verification Code";
                Message.Body = MailBody;
                Message.IsBodyHtml = true;
                SmtpClient SmtpServer = new SmtpClient(SmtpServerAddress, Convert.ToInt32(SmtpServerPort));
                SmtpServer.Credentials = LoginInfo;
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Send(Message);
                return true;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}