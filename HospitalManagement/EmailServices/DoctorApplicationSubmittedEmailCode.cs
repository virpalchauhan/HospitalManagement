using System.Net.Mail;
using System.Net;
namespace HospitalManagement.EmailServices
{
    public class DoctorApplicationSubmittedEmailCode
    {

        private readonly IConfiguration _configuration;

        public DoctorApplicationSubmittedEmailCode(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public  bool DoctorApplicationSubmittedEmailTempletCodeSend(string Recipient, string MailBody)
        {

            string GmailAccountEmail = _configuration["EmailSettings:Email"];
            string GmailAccountPassword = _configuration["EmailSettings:Password"];
            string SmtpServerAddress = _configuration["EmailSettings:SmtpServer"];
            string SmtpServerPort = _configuration["EmailSettings:SmtpPort"];

            try
            {

               
                NetworkCredential LoginInfo = new NetworkCredential(GmailAccountEmail, GmailAccountPassword);

                MailMessage Message = new MailMessage();
                Message.From = new MailAddress(GmailAccountEmail, "Doctor Account Activation");
                Message.To.Add(new MailAddress(Recipient));
                Message.Subject = "Doctor Account Activation";
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
