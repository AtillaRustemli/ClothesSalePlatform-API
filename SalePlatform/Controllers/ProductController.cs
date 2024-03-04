using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.ProductDTOs;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ClothesSalePlatform.DTOs.ProductDTOs.ReturnProductDto;

namespace ClothesSalePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(AppDbContext context, IMapper mapper, IProductService productService)
        {
            _context = context;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
       public IActionResult GetAll(int take=3,int page=1,string? search=null)
        {
            var result=_productService.GetAll(page,take,search,_mapper);
            return Ok(result);
        }
        [HttpGet("{id?}")]
       public IActionResult Get(int? id)
        {
           if(id == null) return BadRequest();
            var product = _context.Products.
                 Where(p => !p.IsDeleted)
                 .Include(p => p.Size)
                 .Include(p => p.Category)
                 .Include(p => p.Brand)
                 .Include(p => p.Store)
                 .Include(p => p.Gender)

                 .FirstOrDefault(p=>p.Id==id);
            if(product == null) return NotFound();

            var result = _mapper.Map<ReturnProductDto>(product);
            return Ok(result);
        }
    }
}
