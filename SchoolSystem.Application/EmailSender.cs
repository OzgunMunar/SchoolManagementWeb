using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application
{
    public class EmailSender : IEmailSender
    {
        public EmailSender()
        {
            
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Will come back here
            return Task.CompletedTask;
        }
    }
}
