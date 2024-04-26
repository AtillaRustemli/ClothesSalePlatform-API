using ClothesSalePlatform.DTOs.EmailDTOs;
using MimeKit;
using System.Net.Mail;

namespace ClothesSalePlatform.Services.EmailServices
{
    public interface IEmailService
    {
        List<MailMessage> CreateEmaill(CreateEmailDto createEmailDto);
        void SendEmaill(List<MailMessage> message);
        void PaymentEmail(string sessionId, string customerEmail,string url);
    }
}
