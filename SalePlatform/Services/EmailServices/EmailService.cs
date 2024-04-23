using ClothesSalePlatform.DTOs.EmailDTOs;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.OtherServices.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Template;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace ClothesSalePlatform.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _config;
        private readonly UserManager<AppUser> _userManager;

        public EmailService(EmailConfig config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public List<MailMessage> CreateEmaill(CreateEmailDto createEmailDto)
        {
            int count = 0;
            var emailMessages= new List<MailMessage>();
            MailMessage message;
           
            string body;
            //using (StreamReader streamReader = new("wwwroot/Templates/VerifyEmail.html"))
            //{
            //    body = streamReader.ReadToEnd();
            //}
            AppUser appUser;
            foreach (var item in createEmailDto.Addresses)
            {
                body =  File.ReadAllText("wwwroot/Templates/VerifyEmail.html");
                appUser = _userManager.FindByEmailAsync(item).Result;
                message = new();
                message.From = new MailAddress(_config.From);
               
                body=body.Replace("{{name}}", appUser.UserName);
                body = body.Replace("{{source}}", createEmailDto.Source);
                body = body.Replace("{{productName}}", createEmailDto.ProductName);
                body = body.Replace("{{productPrice}}", createEmailDto.ProductPrice);
                body = body.Replace("{{productColor}}", createEmailDto.ProductColor);
                message.Body = body;
                message.Subject = createEmailDto.Subject;
                message.To.Add(createEmailDto.Addresses.FirstOrDefault(a=>a==appUser.Email));
                message.IsBodyHtml = true;
                emailMessages.Add(message);
               

            }
         
            return emailMessages;
        }

        public void SendEmaill(List<MailMessage> messages)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = _config.Port;
            smtpClient.Host = _config.SmtpServer;
            smtpClient.EnableSsl = true;
           
            smtpClient.Credentials = new NetworkCredential(_config.From, _config.Password);
            foreach (var message in messages) {
            
            smtpClient.Send(message);
            }
        }
    }
}
