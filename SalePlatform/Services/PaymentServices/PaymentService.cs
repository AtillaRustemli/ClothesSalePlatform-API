using ClothesSalePlatform.Resources;
using Stripe;

namespace ClothesSalePlatform.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {

        private readonly TokenService _tokenService;
        private readonly CustomerService _customerService;
        private readonly ChargeService _chargeService;

        public PaymentService(
            TokenService tokenService,
            CustomerService customerService,
            ChargeService chargeService)
        {
            _tokenService = tokenService;
            _customerService = customerService;
            _chargeService = chargeService;
        }

        public async Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken)
        {
            var tokenOptions = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Name = resource.Card.Name,
                    Number = resource.Card.Number,
                    ExpYear = resource.Card.ExpiryYear,
                    ExpMonth = resource.Card.ExpiryMonth,
                    Cvc = resource.Card.Cvc
                }
            };
            var token = await _tokenService.CreateAsync(tokenOptions, null, cancellationToken);

            var customerOptions = new CustomerCreateOptions
            {
                Email = resource.Email,
                Name = resource.Name,
                Source = token.Id
            };
            var customer = await _customerService.CreateAsync(customerOptions, null, cancellationToken);

            return new CustomerResource(customer.Id, customer.Email, customer.Name);
        }

        public async Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken)
        {
            var chargeOptions = new ChargeCreateOptions
            {
                Currency = resource.Currency,
                Amount = resource.Amount,
                ReceiptEmail = resource.ReceiptEmail,
                Customer = resource.CustomerId,
                Description = resource.Description
            };

            var charge = await _chargeService.CreateAsync(chargeOptions, null, cancellationToken);

            return new ChargeResource(
                charge.Id,
                charge.Currency,
                charge.Amount,
                charge.CustomerId,
                charge.ReceiptEmail,
                charge.Description);
        }







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
