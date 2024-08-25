using Microsoft.Extensions.Configuration;
using SchoolDiary.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.BLL.IServices
{
    public interface IEmailNotificationService
    {
        Task Send(EmailMessageDTO emailMessageDto, IConfigurationSection config);
    }
}
