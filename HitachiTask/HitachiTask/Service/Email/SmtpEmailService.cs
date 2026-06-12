using System.Net;
using System.Net.Mail;
using HitachiTask.Dto.Email;

namespace HitachiTask.Service.Email;

public sealed class SmtpEmailService : EmailService {
    public void SendReport(EmailSettingsDto settingsDto, string report, string subject) {
        using MailMessage message = new();

        message.From = new MailAddress(settingsDto.SenderEmail);
        message.To.Add(settingsDto.ReceiverEmail);

        message.Subject = subject;
        message.Body = report;

        using SmtpClient client = new("smtp.gmail.com", 587);
        client.EnableSsl = true;

        client.Credentials = new NetworkCredential(settingsDto.SenderEmail, settingsDto.Password);
        client.Send(message);
    }
}