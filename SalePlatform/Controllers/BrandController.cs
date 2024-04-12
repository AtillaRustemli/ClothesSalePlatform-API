using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.BrandDTOs;
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
        public BrandController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var brand=_context.Brand
                .Where(b=>!b.IsDeleted)
                .Include(b=>b.BrandCategory)
                .ThenInclude(b=>b.Category)
                .Include(b=>b.BrandStore)
                .ThenInclude(b=>b.Store)
                .ToList();
               
            if(brand==null) return NotFound();

            RetrunBrandListDto returnBrandListDto = new()
            {
                BrandCount = brand.Count,
            };

            returnBrandListDto.Values = _mapper.Map<List<ReturnBrandDto>>(brand);
          
            return Ok(returnBrandListDto);
        }
    }
}
