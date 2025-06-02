using E_Learning.GraduationProject.Core.Hellper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email email);
    }
}
