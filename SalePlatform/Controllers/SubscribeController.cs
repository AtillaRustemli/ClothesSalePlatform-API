using AutoMapper;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.Services.SubscribeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesSalePlatform.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        private readonly ISubscribeService _subscribeService;
        private readonly IMapper _mapper;
        

        public SubscribeController(ISubscribeService subscribeService, IMapper mapper)
        {
            _subscribeService = subscribeService;
            _mapper = mapper;
        }


        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _subscribeService.GetAll(_mapper);
            if(result == null) return BadRequest();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetOne")]
        public IActionResult GetOne(int?id)
        {
            var result = _subscribeService.GetOne(id,_mapper);
            if(result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost("SubscribeBrand")]
        public IActionResult SubscribeBrand(int? brandId)
        {
            if(!User.Identity.IsAuthenticated)
            {
                return BadRequest("Sign in first");
            }
          var result=_subscribeService.SubscribeBrand(brandId,User);


            return StatusCode(result);
        }
        [HttpPost("UnsubscribeBrand")]
        public IActionResult UnsubscribeBrand(int? brandId)
        {
            if(!User.Identity.IsAuthenticated)
            {
                return BadRequest("Sign in first");
            }
          var result=_subscribeService.UnsubscribeBrand(brandId,User);


            return StatusCode(result);
        }
        [HttpPost("SubscribeCategory")]
        public IActionResult SubscribeCategory(int? categoryId)
        {
            var result=_subscribeService.SubscribeCategory(categoryId,User);
            return Ok(result);
        }
        [HttpPost("UnubscribeCategory")]
        public IActionResult UnubscribeCategory(int? categoryId)
        {
            var result=_subscribeService.UnsubscribeCategory(categoryId,User);
            return Ok(result);
        }
        [HttpPost("SubscribeStore")]
        public IActionResult SubscribeStore(int? storeId)
        {
            var result=_subscribeService.SubscribeStore(storeId, User);
            return Ok(result);
        }
        [HttpPost("UnubscribeStore")]
        public IActionResult UnubscribeStore(int? storeId)
        {
            var result=_subscribeService.UnsubscribeStore(storeId, User);
            return Ok(result);
        }
    }
}
