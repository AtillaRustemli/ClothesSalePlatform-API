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

        public void ConfirmEmail(string address, string subject, string url)
        {
            MailMessage message = new();
            message.From= new MailAddress(_config.From);
            string body = File.ReadAllText("wwwroot/Templates/VerifyEmail.html");
            body = body.Replace("{{link}}", url);
            message.IsBodyHtml = true;
            message.Body = body;
            message.Subject = subject;
            message.To.Add(address);


            SmtpClient smtpClient = new();
            smtpClient.Port = _config.Port;
            smtpClient.Host = _config.SmtpServer;
            smtpClient.EnableSsl = true;

            smtpClient.Credentials = new NetworkCredential(_config.From, _config.Password);
            smtpClient.Send(message);


           
        }

        public List<MailMessage> CreateEmaill(CreateEmailDto createEmailDto)
        {
            var emailMessages= new List<MailMessage>();
            MailMessage message;
           
            string body;
            string currentAddress;
            //using (StreamReader streamReader = new("wwwroot/Templates/VerifyEmail.html"))
            //{
            //    body = streamReader.ReadToEnd();
            //}
            AppUser appUser;
            foreach (var item in createEmailDto.Addresses)
            {
                body =  File.ReadAllText("wwwroot/Templates/EmailNotification.html");
                appUser = _userManager.FindByEmailAsync(item).Result;
                message = new();
                message.From = new MailAddress(_config.From);
               if(appUser != null)
                {
                   body=body.Replace("{{name}}", appUser.UserName);
                message.To.Add(createEmailDto.Addresses.FirstOrDefault(a=>a==appUser.Email));
                emailMessages.Add(message);
                }
               
                body = body.Replace("{{source}}", createEmailDto.Source);
                body = body.Replace("{{productName}}", createEmailDto.ProductName);
                body = body.Replace("{{productPrice}}", createEmailDto.ProductPrice);
                body = body.Replace("{{productColor}}", createEmailDto.ProductColor);
                message.Body = body;
                message.Subject = createEmailDto.Subject;
                message.IsBodyHtml = true;
               

            }
         
            return emailMessages;
        }

        public void PaymentEmail(string sessionId, string customerEmail, string url)
        {
            
            MailMessage message = new()
            {
                Body=url,
                Subject="Click link below to finish payment:",
                From = new MailAddress(_config.From),

        };
            message.To.Add(customerEmail);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = _config.Port;
            smtpClient.Host = _config.SmtpServer;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_config.From, _config.Password);
            smtpClient.Send(message);
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
