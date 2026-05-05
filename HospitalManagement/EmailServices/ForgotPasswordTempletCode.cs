using System.Net;
using System.Net.Mail;

namespace HospitalManagement.EmailServices
{
    public class ForgotPasswordTempletCode
    {

        public static bool ForgotPasswordTempletCodeSend(string Recipient, string MailBody)
        {
            try
            {
                string GmailAccountEmail = "virpalsinhchauhan007@gmail.com";
                string GmailAccountPassword = "pydeblghacbtutnp";
                NetworkCredential LoginInfo = new NetworkCredential(GmailAccountEmail, GmailAccountPassword);


                MailMessage Message = new MailMessage();
                Message.From = new MailAddress(GmailAccountEmail, "Password Reset OTP");
                Message.To.Add(new MailAddress(Recipient));
                Message.Subject = "Password Reset OTP";
                Message.Body = MailBody;
                Message.IsBodyHtml = true;
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
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
