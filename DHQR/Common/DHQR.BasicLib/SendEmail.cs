using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace DHQR.BasicLib
{
    public class SendMailModelService
    {
        public static void Send(SendMailModel model)
        {
            MailAddress from = new MailAddress(model.EmailName);
            MailAddress toAdd = new MailAddress(model.MailToAdd);
            MailMessage mailMessage = new MailMessage(from, toAdd); //邮件信息 
            mailMessage.Subject = model.EmailSubject;
            mailMessage.Body = model.EmailBody;
            if (model.EmailAddachFiles != null)
            {
                //附件
                foreach (var item in model.EmailAddachFiles)
                {
                    mailMessage.Attachments.Add(new Attachment(item));
                }
            }
            mailMessage.IsBodyHtml = true;
            SmtpClient SmtpClient = new SmtpClient(model.EmailHost);
            SmtpClient.UseDefaultCredentials = false;
            SmtpClient.Credentials = new NetworkCredential(model.EmailName, model.EmailPsd);
            SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                SmtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

    public class SendMailModel
    {
        // public string EmailUser { get; set; }
        public string EmailName { get; set; }
        public string EmailPsd { get; set; }
        public string EmailSubject { get; set; }

        public string[] EmailAddachFiles { get; set; }

        public string EmailBody { get; set; }

        public string EmailHost { get; set; }

        public string MailToAdd { get; set; }
    }
}
