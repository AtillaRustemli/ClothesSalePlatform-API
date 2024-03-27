using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.CategoryDTOs;
using ClothesSalePlatform.Services.CategoryServices;
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
        private readonly ICategoryService _categoryService;

        public CategoryController(AppDbContext context, IMapper mapper, ICategoryService categoryService)
        {
            _context = context;
            _mapper = mapper;
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _categoryService.GetAll(_mapper);
            if (result == null) return BadRequest();

            return Ok(result);
        }


        [HttpGet("{id?}")]
        public IActionResult Get(int?id)
        {
            if (id == null) return BadRequest();
            var category = _categoryService.GetOne(_mapper, id);
            if(category== null) return NotFound();
            return Ok(category);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            return StatusCode(_categoryService.Delete(id));
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateCategoryDto createCategoryDto)
        {
            return StatusCode(_categoryService.Create(createCategoryDto,_mapper));
        }
        [HttpPut("Update/{id?}")]
        public IActionResult Update(int?id,UpdateCategoryDto updateCategoryDto)
        {

            return StatusCode(_categoryService.Update(id,updateCategoryDto,_mapper));
        }
    }
}
