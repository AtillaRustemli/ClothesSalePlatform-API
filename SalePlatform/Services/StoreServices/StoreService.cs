using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.StoreDTOs;
using Microsoft.EntityFrameworkCore;

namespace ClothesSalePlatform.Services.StoreServices
{
    public class StoreService : IStoreService
    {
        private readonly AppDbContext _context;

        public StoreService(AppDbContext context)
        {
            _context = context;
        }

        public ReturnStoreListDto GetAll(IMapper _mapper)
        {
            var store = _context.Store
                 .Where(s => !s.IsDeleted)
                 .Include(s => s.BrandStore)
                 .ThenInclude(s => s.Brand)
                 .Include(s => s.StoreCategory)
                 .ThenInclude(s => s.Category)
                 .Include(s => s.Products)
                 .ToList();
            if (store == null) return null;
            ReturnStoreListDto returnStoreListDto = new()
            {
                StoreCount = store.Count
            };
            returnStoreListDto.Values= _mapper.Map<List<ReturnStoreDto>>(store);
            foreach (var returnStoreDto in returnStoreListDto.Values)
            {
            foreach (var category in returnStoreDto.CategoryInStoreDto)
            {
                category.ProductCount = _context.Products.Where(p => p.Category.Name == category.Name && p.Store.Name == returnStoreDto.Name && !p.IsDeleted).ToList().Count;
            }
            foreach (var brand in returnStoreDto.BrandInStoreDto)
            {
                brand.ProductCount = _context.Products.Where(p => p.Brand.Name == brand.Name && p.Store.Name == returnStoreDto.Name && !p.IsDeleted).ToList().Count;
            }

            }
            return returnStoreListDto;
        }

        public ReturnStoreDto GetOne(int? id, IMapper _mapper)
        {
           var store=_context.Store
                .Where(s=>!s.IsDeleted)
                .Include(s=>s.BrandStore)
                .ThenInclude(s=>s.Brand)
                .Include(s=>s.StoreCategory)
                .ThenInclude(s=>s.Category)
                .Include(s=>s.Products)
                .FirstOrDefault(s=>s.Id==id);
            if (store == null) return null;
            var returnStoreDto =_mapper.Map<ReturnStoreDto>(store);  

            foreach (var category in returnStoreDto.CategoryInStoreDto)
            {
                category.ProductCount = _context.Products.Where(p => p.Category.Name == category.Name && p.StoreId == id&&!p.IsDeleted).ToList().Count;
            }
            foreach (var brand in returnStoreDto.BrandInStoreDto)
            {
                brand.ProductCount = _context.Products.Where(p => p.Brand.Name == brand.Name && p.StoreId == id&&!p.IsDeleted).ToList().Count;
            }


            return returnStoreDto;
        }
    }
}
