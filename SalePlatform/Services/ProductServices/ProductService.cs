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
                .Skip((page - 1) * take)
                .Take(take)
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
            return returnProductList;
        }
        public ReturnProductListDto Create(CreateProductDto createProductDto, IMapper _mapper)
        {
            throw new NotImplementedException();
        }

        public ReturnProductListDto Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public ReturnProductListDto Get(int? id, IMapper _mapper)
        {
            throw new NotImplementedException();
        }

    }
}
