using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.PaymentDTOs;
using ClothesSalePlatform.Resources;
using ClothesSalePlatform.Services.EmailServices;
using Microsoft.AspNetCore.Mvc;
using Stripe;
//using Stripe.BillingPortal;
using Stripe.Checkout;
using System.Security.Claims;

namespace ClothesSalePlatform.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {

        //private readonly TokenService _tokenService;
        //private readonly CustomerService _customerService;
        //private readonly ChargeService _chargeService;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public PaymentService(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public string Cancel()
        {
            return "Payment was cancelled";
        }

        public  Session CreateSession([FromBody] List<ProducInfoDto> producInfoDto, ClaimsPrincipal user)
        {
            var lineItems = new List<SessionLineItemOptions>();
            ClothesSalePlatform.Models.Product product;
            foreach (var item in producInfoDto)
            {
                product=_context.Products.Where(p=>!p.IsDeleted).FirstOrDefault(p=>p.Id==item.ProductId);   
                lineItems.Add(
                     new SessionLineItemOptions
                     {
                         PriceData = new SessionLineItemPriceDataOptions
                         {
                             Currency = "usd",
                             ProductData = new SessionLineItemPriceDataProductDataOptions
                             {
                                 Name =product.Name,
                             },
                             UnitAmount = (int)Math.Round(product.Price*100), // Qəpikdir (100-ə böl)
                         },
                         Quantity = item.Quantity,
                     }
                    );
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
            {
                "card",
            },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://7368-188-253-212-11.ngrok-free.app/api/payment/success?sessionId={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://7368-188-253-212-11.ngrok-free.app/api/payment/cancel",
                CustomerEmail= user.FindFirstValue(ClaimTypes.Email)
            };

            var service = new SessionService();
            Session session = service.Create(options);
            _emailService.PaymentEmail(session.Id, user.FindFirstValue(ClaimTypes.Email));
            return session;
           
        }

        public Session Success(string sessionId)
        {
            var service = new Stripe.Checkout.SessionService();
            var session = service.Get(sessionId);

            if (session.PaymentStatus == "paid")
            {
               

                return session; 
            }
            else
            {
                return null;
            }
        }

        #region Second option
        //public async Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken)
        //{
        //    StripeClient a;
        //    var tokenOptions = new TokenCreateOptions
        //    {
        //        Card = new TokenCardOptions
        //        {
        //            Name = resource.Card.Name,
        //            Number = resource.Card.Number,
        //            ExpYear = resource.Card.ExpiryYear,
        //            ExpMonth = resource.Card.ExpiryMonth,
        //            Cvc = resource.Card.Cvc
        //        }
        //    };
        //    var token = await _tokenService.CreateAsync(tokenOptions, null, cancellationToken);

        //    var customerOptions = new CustomerCreateOptions
        //    {
        //        Email = resource.Email,
        //        Name = resource.Name,
        //        Source = token.Id

        //    };
        //    var customer = await _customerService.CreateAsync(customerOptions, null, cancellationToken);

        //    return new CustomerResource(customer.Id, customer.Email, customer.Name);
        //}

        //public async Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken)
        //{
        //    var chargeOptions = new ChargeCreateOptions
        //    {
        //        Currency = resource.Currency,
        //        Amount = resource.Amount,
        //        ReceiptEmail = resource.ReceiptEmail,
        //        Customer = resource.CustomerId,
        //        Description = resource.Description
        //    };

        //    var charge = await _chargeService.CreateAsync(chargeOptions, null, cancellationToken);

        //    return new ChargeResource(
        //        charge.Id,
        //        charge.Currency,
        //        charge.Amount,
        //        charge.CustomerId,
        //        charge.ReceiptEmail,
        //        charge.Description);
        //}
        #endregion









        //public string CreatePayment(string stripeEmail, string stripeToken, double amount = 1)
        //{
        //    var customers = new CustomerService();
        //    var charges = new ChargeService();
        //    var customer = customers.Create(new CustomerCreateOptions
        //    {
        //        Email = stripeEmail,
        //        Source = stripeToken,
        //    });

        //    var charge = charges.Create(new ChargeCreateOptions
        //    {
        //        Amount = (long)amount * 100,
        //        Description = "Hello",
        //        Currency = "usd",
        //        Customer = customer.Id,
        //        ReceiptEmail = stripeEmail,
        //        Metadata = new Dictionary<string, string>()
        //        {
        //            {"OrderId","111"},
        //            {"Postcode","LEE111"}
        //        }
        //    });
        //    if (charge.Status == "Successed")
        //    {
        //        string balanceTransition = charge.BalanceTransactionId;
        //        return balanceTransition;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
