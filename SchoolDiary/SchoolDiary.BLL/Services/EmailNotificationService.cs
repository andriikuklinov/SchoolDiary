using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SchoolDiary.BLL.DTO;
using SchoolDiary.BLL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.BLL.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private MimeMessage CreateMimeMessage(EmailMessageDTO emailMessageDto)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(emailMessageDto.Sender);
            mimeMessage.To.Add(emailMessageDto.Reciever);
            mimeMessage.Subject = emailMessageDto.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            { Text = emailMessageDto.Content };
            return mimeMessage;
        }
        public async Task Send(EmailMessageDTO emailMessageDto, IConfigurationSection config)
        {
            var mimeMessage = CreateMimeMessage(emailMessageDto);

            using(SmtpClient smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(config["SmtpServer"], int.Parse(config["Port"]), true);
                await smtpClient.AuthenticateAsync(config["Username"], config["Password"]);
                await smtpClient.SendAsync(mimeMessage);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}
