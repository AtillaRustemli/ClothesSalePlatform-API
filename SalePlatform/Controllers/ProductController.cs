using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.ProductDTOs;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ClothesSalePlatform.DTOs.ProductDTOs.ReturnProductDto;

namespace ClothesSalePlatform.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
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
            var result=_productService.GetAll(page,take,search,_mapper)
                
                ;
            return Ok(result);
        }
        [HttpGet("{id?}")]
       public IActionResult Get(int? id)
        {
            if (id == null) return BadRequest();
            var result=_productService.Get(id,_mapper);
            return Ok(result);
        }
        [HttpPost("Create")]
        public IActionResult Create(CreateProductDto createProductDto)
        {
            var result=_productService.Create(createProductDto,_mapper);
            return StatusCode(result) ;
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var results=_productService.Delete(id);
            return StatusCode(results);
        }
        [HttpPut("Update/{id?}")]
        public IActionResult Update(UpdateProductDto updateProductDto,int?id)
        {
            var result= _productService.Update(updateProductDto,id,_mapper);
            return StatusCode(result);
        }
    }
}
