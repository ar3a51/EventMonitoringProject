using System;
using System.Net.Mail;


namespace EventMonitoringWS.Email
{
    public class EmailNotification:IDisposable
    {
       
        private String recipient;
        private String cc;
        private String message;
        private String subject;
        private SmtpClient mailClient;
        private MailMessage mail;
        private MailAddress addressFrom;
        //private MailAddress addressTo;
        //private NetworkCredential NetworkCred;


        public EmailNotification(String smtpHost, String from, String displayName)
        {
            
            mailClient = new SmtpClient(smtpHost);
           // NetworkCred = CredentialCache.DefaultNetworkCredentials;
            //mailClient.Credentials = NetworkCred;
            

            addressFrom = new MailAddress(from,
                                               displayName);
            
        }

        public void setSubject(String subject)
        {
            subject = subject;
        }
        public void setRecipient(String recipient)
        {
            recipient = recipient;
        }

        public void setCC(String CCrecipient)
        {
            cc = CCrecipient;
        }

        public void setMessage(String message)
        {
            message = message;
        }

        public Boolean sendMail()
        {
            

            
            //addressTo = new MailAddress(recipient);

            mail = new MailMessage();

            //mail.Sender = addressFrom;
            mail.From = addressFrom;
            //mail = new MailMessage(addressFrom,addressTo);
            mail.To.Add(recipient);
            //mail.From = new MailAddress("itsupport@charteredaccountants.com.au", "IT Support");
            mail.Priority = MailPriority.High;
            mail.Subject = subject;
            //mail.To = addressCollection;
            mail.Body = message;
            
            
            //mail = new MailMessage(
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.Send(mail);
            return true;

        }

        public void Dispose()
        {
            addressFrom = null;
            mail = null;
            mailClient = null;
            
        }
    }
}