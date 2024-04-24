using ClothesSalePlatform.Resources;
using Stripe.Checkout;

namespace ClothesSalePlatform.Services.PaymentServices
{
    public interface IPaymentService
    {
        //string CreatePayment(string stripeEmail, string stripeToken, double amount = 1);

        Session Success(string sessionId);
        string Cancel();
        Task CreateSession();


        //Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken);
        //Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken);
    }
}
