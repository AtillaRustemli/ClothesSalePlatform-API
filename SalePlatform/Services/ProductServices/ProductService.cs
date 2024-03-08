using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.ProductDTOs;
using ClothesSalePlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothesSalePlatform.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public ReturnProductListDto GetAll(int page, int take, string? search, IMapper _mapper)
        {
            var products = _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Size)
                .Include(p => p.Gender)
                .Include(p => p.Store)
                .ToList();
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            List<Category> categories = new();
            foreach (var item in products)
            {
                categories.Add(item.Category);
            }
            var returnProductList = new ReturnProductListDto();
            returnProductList.TotalCount = products.Count;
            returnProductList.Items = _mapper.Map<List<ReturnProductDto>>(products);
            returnProductList.Items=returnProductList.Items
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();
            return returnProductList;
        }
        public int Create(CreateProductDto createProductDto, IMapper _mapper)
        {

            if (createProductDto == null) return 404;
            var product = _mapper.Map<Product>(createProductDto);
            if(createProductDto.ProductCount > 0)
                product.InStock= true;
            else
                product.InStock= false;


            
            _context.Products.Add(product);
            _context.SaveChanges();
          

            return 201;
        }

        public int Delete(int? id)
        {

            if (id == null) return 400;
          var product=_context.Products.FirstOrDefault(p=>p.Id==id);
            if (product == null) return 404;
            product.IsDeleted = true;
            product.DeletedAt = DateTime.Now;
            _context.SaveChanges();
            return 204;
        }

        public ReturnProductDto Get(int? id, IMapper _mapper)
        {
            
            var product = _context.Products.
                 Where(p => !p.IsDeleted)
                 .Include(p => p.Size)
                 .ThenInclude(p => p.Product)
                 .Include(p => p.Category)
                 .ThenInclude(pc => pc.Products)
                 .Include(p => p.Brand)
                 .ThenInclude(p => p.Products)
                 .Include(p => p.Store)
                 .ThenInclude(p => p.Products)
                 .Include(p => p.Gender)
                 .ThenInclude(p => p.Product)

                 .FirstOrDefault(p => p.Id == id);
            if (product == null) return null;

            var result = _mapper.Map<ReturnProductDto>(product);
            return result;
        }

        public int Update(UpdateProductDto udateProductDto, int?id,IMapper _mapper)
        {
            if (udateProductDto is null) return 400;
            if (id is null) return 400;
            var product=_context.Products.Where(p=>!p.IsDeleted).FirstOrDefault(p => p.Id == id);
            if (product is null) return 404;
            if (udateProductDto.ProductCount == 0) udateProductDto.InStock = false;
            _mapper.Map(udateProductDto, product);
            _context.SaveChanges();

            return 202;
        }
    }
}
