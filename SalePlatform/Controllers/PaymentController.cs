using ClothesSalePlatform.Resources;
using ClothesSalePlatform.Services.PaymentServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesSalePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("customer")]
        public async Task<ActionResult<CustomerResource>> CreateCustomer([FromBody] CreateCustomerResource resource, CancellationToken cancellationToken)
        {
            var response = await _paymentService.CreateCustomer(resource, cancellationToken);
            return Ok(response);
        }

        [HttpPost("charge")]
        public async Task<ActionResult<ChargeResource>> CreateCharge([FromBody] CreateChargeResource resource, CancellationToken cancellationToken)
        {
            var response = await _paymentService.CreateCharge(resource, cancellationToken);
            return Ok(response);
        }
    }
}
