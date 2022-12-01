using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebSite.Services.MailServices
{
    public class SendMailServices : ISendMailServices
    {
        private readonly MailSetting mailSetting;
        private readonly ILogger<SendMailServices> logger;
        public SendMailServices(IOptions<MailSetting> options, ILogger<SendMailServices> logger)
        {
            mailSetting = options.Value;
            this.logger = logger;
        }

        public Task SendMail(MailContent content)
        {
            throw new NotImplementedException();
        }

     
        public async Task<bool> SendMailAsync( MailContent content)
        {
            var task = new Task<bool>(() =>
            {
                var hots = mailSetting.Host.Trim();
                var mailTo = content.To.Trim();
                var sender = mailSetting.Mail.Trim();
                var password = mailSetting.Password.Trim();
                var subject = content.Subject;
                var body = content.Body;

                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                NetworkCredential credentials = new NetworkCredential(sender, password);
                client.EnableSsl = true;
                client.Credentials = credentials;

                var mail = new MailMessage(sender, mailTo);
                mail.IsBodyHtml = true;
                mail.Subject = subject;
                mail.Body = content.Body;
                try
                {
                    
                    client.Send(mail);
                    return true;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    logger.LogError(mail.Body);
                    return false;
                }

            });

            task.Start();

            return await task;
        }

    }
}
