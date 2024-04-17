using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.StoreDTOs;
using ClothesSalePlatform.Services.BrandServices;
using ClothesSalePlatform.Services.StoreServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesSalePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStoreService _storeService;

        public StoreController(AppDbContext context, IMapper mapper, IStoreService storeService)
        {
            _context = context;
            _mapper = mapper;
            _storeService = storeService;
        }
        [HttpGet]

        public IActionResult Get()
        {

            var result = _storeService.GetAll ( _mapper);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpGet("{id?}")]

        public IActionResult GetOne(int?id)
        {
            if (id == null) return BadRequest();
            var result = _storeService.GetOne(id, _mapper);
            if(result == null) return NotFound();
            return Ok(result);
        }
        [HttpDelete("{id?}")]
        public IActionResult Delete(int? id)
        {
            return StatusCode(_storeService.Delete(id));
        }
        [HttpPost("Create")]
        public IActionResult Create(CreateStoreDto createStoreDto)
        {
            var newStore=_storeService.Create(createStoreDto,_mapper);
            return StatusCode(newStore);
        }
        [HttpPut("Update/{id?}")]
        public IActionResult Update(int?id,UpdateStoreDto updateStoreDto)
        {
            return StatusCode(_storeService.Update(id, updateStoreDto, _mapper));
        }


    }
}
