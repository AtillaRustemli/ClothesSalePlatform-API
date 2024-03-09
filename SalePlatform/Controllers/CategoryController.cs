using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothesSalePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id?}")]
        public IActionResult Get(int?id)
        {
            if (id == null) return BadRequest();
            var product=_context.Categories
                .Where(c=>!c.IsDeleted)
                .Include(c=>c.BrandCategory)
                .ThenInclude(c=>c.Brand)
                .Include(c=>c.StoreCategory)
                .ThenInclude(c=>c.Store)
                .FirstOrDefault(x => x.Id == id);

            var result = _mapper.Map<ReturnCategoryDto>(product);
          for (var i = 0;i< result.BrandInCategoryDto.Length;i++)
            {
                result.BrandInCategoryDto[i].CategoryProductCount=_context.Products.Where(p=>p.CategoryId==id&&p.Brand.Name== result.BrandInCategoryDto[i].Name).Count();
            }
          for (var i = 0;i< result.StoreInCategoryDto.Length;i++)
            {
                result.StoreInCategoryDto[i].CategoryProductCount=_context.Products.Where(p=>p.CategoryId==id&&p.Store.Name== result.StoreInCategoryDto[i].Name).Count();
            }
            return Ok(result);
        }
    }
}
