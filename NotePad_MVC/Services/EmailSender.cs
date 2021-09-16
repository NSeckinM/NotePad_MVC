using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NotePad_MVC.Services
{

    //Gmail hesabı kullanarak mail göndermek için 
    //smtp adresi = smtp.gamil.com
    // port = 587;
    //kullanıcı adı = *****@gmail.com
    //parola = *******
    //gmail ayarlarından güvenli olmayan uygulamalara izin ver demelisin aşağıdaki linkten !!!
    //https://www.google.com/settings/security/lesssecureapps
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            string sender = "******@gmail.com";//kullanıcı adı

            MailMessage mail = new MailMessage(sender, email, subject, htmlMessage);
            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))//gönderici maili
            {
                smtp.Credentials = new NetworkCredential("*******@gmail.com", "password");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }

        }
    }
}
