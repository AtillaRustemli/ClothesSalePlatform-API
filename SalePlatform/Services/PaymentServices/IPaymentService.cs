﻿using ClothesSalePlatform.Resources;

namespace ClothesSalePlatform.Services.PaymentServices
{
    public interface IPaymentService
    {
        //string CreatePayment(string stripeEmail, string stripeToken, double amount = 1);
        Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken);
        Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken);
    }
}