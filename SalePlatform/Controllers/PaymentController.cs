using ClothesSalePlatform.DTOs.PaymentDTOs;
using ClothesSalePlatform.Services.PaymentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothesSalePlatform.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("Success")]
        public IActionResult Success(string sessionId)//[FromBody] CreateCustomerResource resource, CancellationToken cancellationToken
        {
           // var response = await _paymentService.CreateCustomer(resource, cancellationToken);
           var resuly=_paymentService.Success(sessionId);
            
            return Ok();
        }

        [HttpGet("Cancel")]
        public IActionResult Cancel()//[FromBody] CreateChargeResource resource, CancellationToken cancellationToken
        {
            //var response = await _paymentService.CreateCharge(resource, cancellationToken);
            var response = _paymentService.Cancel();
            return Ok(response);
        }

        [HttpPost("CreateSession")]
        public IActionResult CreateSession([FromBody]List<ProducInfoDto> producInfoDto)//[FromBody] CreateChargeResource resource, CancellationToken cancellationToken
        {
            //var response = await _paymentService.CreateCharge(resource, cancellationToken);
            var response = _paymentService.CreateSession(producInfoDto,User);
            return Ok(response);
        }
    }
}
