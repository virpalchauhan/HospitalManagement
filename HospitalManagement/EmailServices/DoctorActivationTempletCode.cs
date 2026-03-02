using System.Net.Mail;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace HospitalManagement.EmailServices
{
    public class DoctorActivationTempletCode
    {

       

        public static bool DoctorActivationTempletCodeSend(string Recipient,string MailBody)
        {
            try
            {

                string GmailAccountEmail = "virpalsinhchauhan007@gmail.com";
                string GmailAccountPassword = "pydeblghacbtutnp";
                NetworkCredential LoginInfo = new NetworkCredential(GmailAccountEmail, GmailAccountPassword);

                MailMessage Message = new MailMessage();
                Message.From = new MailAddress(GmailAccountEmail,"Test Eamil");
                Message.To.Add(new MailAddress(Recipient));
                Message.Subject = "Doctor Account Activation";
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
