using ClothesSalePlatform.DTOs.PaymentDTOs;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace ClothesSalePlatform.Services.PaymentServices
{
    public interface IPaymentService
    {
        //string CreatePayment(string stripeEmail, string stripeToken, double amount = 1);

        Session Success(string sessionId);
        string Cancel();
        Session CreateSession([FromBody] List<ProducInfoDto> producInfoDto, ClaimsPrincipal user);


        //Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken);
        //Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken);
    }
}
