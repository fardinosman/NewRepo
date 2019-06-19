using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Kraer.DifferentServices
{
   public class Email
    {
        public static void SendMail(string sendermail, string senderpass, string recivermail, string subject, string mailbody)
        {

            //  if (String.IsNullOrWhiteSpace(senderpass) && !sendermail.Contains("@")) throw new ArgumentException(nameof(sendermail));
            if (String.IsNullOrWhiteSpace(senderpass)) throw new ArgumentException(nameof(senderpass));
            if (String.IsNullOrWhiteSpace(subject)) throw new ArgumentException(nameof(subject));
            // if (String.IsNullOrWhiteSpace(mailbody)) throw new ArgumentException(nameof(mailbody));
            if (String.IsNullOrWhiteSpace(recivermail)) throw new ArgumentException(nameof(recivermail));



            SmtpClient hotmailClient = new SmtpClient("smtp.live.com", 25); //we must enable ssl
            hotmailClient.EnableSsl = true;
            hotmailClient.Credentials = new NetworkCredential(sendermail, senderpass);
            try
            {


                hotmailClient.Send(sendermail, recivermail, subject, mailbody);
                Console.WriteLine("email sendt successfully");
            }
            catch (Exception ex)
            {

                Console.WriteLine("email cant send successfylly under below reasons");
                //Logging.LogAction("KraerEmailError", "EmailError", ex.Message);//
                Console.WriteLine(ex.Message);

            }


        }
    }
}
