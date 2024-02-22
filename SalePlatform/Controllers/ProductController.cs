using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothesSalePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
       public IActionResult GetAll()
        {
            var products=_context.Products
                .Where(p=>!p.IsDeleted)
                .Include(p=>p.Category)
                .Include(p=>p.Brand)
                .Include(p=>p.Size)
                .Include(p=>p.Gender)
                .Include(p=>p.Store)
                .ToList();
            var returnProductList = new ReturnProductListDto();
            returnProductList.TotalCount= products.Count;
            returnProductList.Items = _mapper.Map<List<ReturnProductDto>>(products);

            return Ok(returnProductList);
        }
    }
}
