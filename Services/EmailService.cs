using MailKit.Net.Smtp;
using MimeKit;
using MyEmailApp.Entities;
using System.Text.Json;

namespace MyEmailApp.Services
{
    public class EmailService
    {
        public async Task SendEmail(EmailModel email, string ToEmail)
        {
            var EmailMessage = new MimeMessage();
            string? userSettingsJson = await SecureStorage.GetAsync("UserSettings");

            if(userSettingsJson is null)
            {
                await Shell.Current.DisplayAlert("Erro", "Usuário não configurado!","Ok");
                return;
            }

            UserSettings userEmailSettings = JsonSerializer.Deserialize<UserSettings>(userSettingsJson)!;

            EmailMessage.From.Add(new MailboxAddress(userEmailSettings.PersonName, userEmailSettings.Email));
            EmailMessage.To.Add(new MailboxAddress("Destinatário", ToEmail));

            EmailMessage.Subject = email.Subject;
            EmailMessage.Body = new TextPart("plain") { Text = email.Message! };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, false);

            await smtp.AuthenticateAsync(userEmailSettings.Email, userEmailSettings.AppPassword);

            await smtp.SendAsync(EmailMessage);
            await smtp.DisconnectAsync(true);
        }
    }
}
