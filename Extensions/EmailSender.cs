using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;

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

            //    var emailSettings = _configuration.GetSection("EmailSettings");

            //    var mimeMessage = new MimeMessage();
            //    mimeMessage.From.Add(new MailboxAddress(emailSettings["FromName"], emailSettings["FromEmail"]));
            //    mimeMessage.To.Add(MailboxAddress.Parse(to));
            //    mimeMessage.Subject = subject;
            //    mimeMessage.Body = new TextPart("plain") { Text = message };

            //    using var client = new System.Net.Mail.SmtpClient();
            //    await client.ConnectAsync(emailSettings["SmtpServer"], Convert.ToInt32(emailSettings["SmtpPort"]), true);
            //    await client.AuthenticateAsync(emailSettings["FromEmail"], emailSettings["SmtpPassword"]);
            //    await client.SendAsync(mimeMessage);
            //    await client.DisconnectAsync(true);
            //    var msg = new MimeMessage();
            //    var mailFrom = new MailboxAddress("Admin", "uomostore004@gmail.com");
            //    var mailTo = new MailboxAddress("User", to);

            //    //msg.From.Add(mailFrom);
            //    //msg.To.Add(mailTo);
            //    //var bodyBuilder = new BodyBuilder();
            //    //bodyBuilder.TextBody = "Confirm Code:" + 77;
            //    //msg.Body = bodyBuilder.ToMessageBody();
            //    //msg.Subject = "Project Confirm Code";

            //    //SmtpClient client = new SmtpClient();
            //    //client.Connect(emailSettings["SmtpServer"], Convert.ToInt32(emailSettings["SmtpPort"]), false);
            //    //client.Authenticate(emailSettings["FromEmail"], emailSettings["SmtpPassword"]);
            //    //client.Send(msg);
            //    //client.Disconnect(true);
            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine($"Error: {ex}");
            //}
        }
    }


}