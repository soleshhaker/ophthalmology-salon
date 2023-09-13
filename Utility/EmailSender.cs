using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class EmailSender : IEmailSender
    {
        //just to shut up identity :)
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //send email
            return Task.CompletedTask;
        }
    }
}
