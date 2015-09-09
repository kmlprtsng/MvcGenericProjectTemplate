using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Project.Web.Infrastructure
{
    public class SmtpEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var text = message.Body;
            var html = message.Body;
            
            var msg = new MailMessage {From = new MailAddress("me@somesite.com")};
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            
            var smtpClient = new SmtpClient();
            smtpClient.Send(msg);

            return Task.FromResult(0);
        }
    }
}