using MailKit.Net.Smtp;
using MimeKit;
using BinaAz.Models.ViewModels;

namespace BinaAz.Extensions
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string message);
    }
    public class EmailSender : IEmailService
    {
        public readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string to, string subject, string message)
        {
            //try
            //{

            //var emailSettings = _configuration.GetSection("EmailSettings");

            //var mimeMessage = new MimeMessage();
            //mimeMessage.From.Add(new MailboxAddress(emailSettings["FromName"], emailSettings["FromEmail"]));
            //mimeMessage.To.Add(MailboxAddress.Parse(to));
            //mimeMessage.Subject = subject;
            //mimeMessage.Body = new TextPart("plain") { Text = message };

            //using var client = new SmtpClient();
            //await client.ConnectAsync(emailSettings["SmtpServer"], Convert.ToInt32(emailSettings["SmtpPort"]), true);
            //await client.AuthenticateAsync(emailSettings["FromEmail"], emailSettings["SmtpPassword"]);
            //await client.SendAsync(mimeMessage);
            //await client.DisconnectAsync(true);
            //var msg = new MimeMessage();
            //var mailFrom = new MailboxAddress("Admin", "uomostore004@gmail.com");
            //var mailTo = new MailboxAddress("User", to);

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Ahliman", "ehliman270@gmail.com"));
            mimeMessage.To.Add(MailboxAddress.Parse(to));
            mimeMessage.Subject = "subject";
            mimeMessage.Body = new TextPart("html") { Text = message };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync("ehliman270@gmail.com", "kimu dbwb lvtt jvxe");
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
            var msg = new MimeMessage();
            var mailFrom = new MailboxAddress("Admin", "ehliman270@gmail.com");
            var mailTo = new MailboxAddress("User", to);
            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine($"Error: {ex}");
            //}
        }
    }


}