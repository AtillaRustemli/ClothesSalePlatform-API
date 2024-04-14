using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.BrandDTOs;
using ClothesSalePlatform.Services.BrandServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothesSalePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBrandService _brandService;
        public BrandController(AppDbContext context, IMapper mapper, IBrandService brandService)
        {
            _context = context;
            _mapper = mapper;
            _brandService = brandService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAll(_mapper);
          if(result==null) return NotFound();
            return Ok(result);
        }
        [HttpGet("{id?}")]
        public IActionResult Get(int?id)
        {
            if(id==null) return BadRequest();
            var result=_brandService.GetOne(id, _mapper);
            if(result==null) return NotFound();

            return Ok(result);
        }
        [HttpPost("Create")]
        public IActionResult Create(CreateBrandDto dto)
        {
            return StatusCode(_brandService.Create(dto, _mapper));
        }
        [HttpPut("Update/{id?}")]
        public IActionResult Update(int?id,UpdateBrandDto dto)
        {
            return StatusCode(_brandService.Update(id,dto, _mapper));
        }
        [HttpDelete("{id?}")]
        public IActionResult Delete(int? id)
        {
            return StatusCode(_brandService.Delete(id));
        }

    }
}
