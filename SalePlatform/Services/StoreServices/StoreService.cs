using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.StoreDTOs;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.Models.ReletionTables;
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

        public int Create(CreateStoreDto createStoreDto, IMapper _mapper)
        {
            if (createStoreDto == null) return 400;
            var store=_mapper.Map<Store>(createStoreDto);
            _context.Store.Add(store);
            BrandStore brandStore;
            StoreCategory storeCategory;
            foreach (var category in createStoreDto.Categories)
            {
                storeCategory = new()
                {
                    Store = store,
                    CategoryId = category
                };
                _context.StoreCategory.Add(storeCategory);
            }
            foreach (var brand in createStoreDto.Brands)
            {
                brandStore = new()
                {
                    Store = store,
                    BrandId = brand
                };
                _context.BrandStore.Add(brandStore);
            }
            _context.SaveChanges();
            return 201;
        }

        public int Delete(int? id)
        {
            if (id == null) return 400;
            var store= _context.Store.Where(s=>!s.IsDeleted).FirstOrDefault(s=>s.Id==id);
            if (store == null) return 404;
            store.IsDeleted = true;
            store.DeletedAt = DateTime.Now;
            _context.SaveChanges();
            return 204;
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

        public int Update(int?id,UpdateStoreDto updateStoreDto, IMapper _mapper)
        {
            if (updateStoreDto == null) return 400;
            var store =_context.Store.Where(s=>!s.IsDeleted).FirstOrDefault(s=>s.Id==id);
            if (store == null) return 404;
           _mapper.Map(updateStoreDto, store);
            BrandStore brandStore;
            StoreCategory storeCategory;
            List<BrandStore> brandStores = _context.BrandStore.Where(s => s.StoreId == id&&!s.IsDeleted).ToList(); 
            List<StoreCategory> storeCategories = _context.StoreCategory.Where(s => s.StoreId == id&&!s.IsDeleted).ToList(); 
            foreach (var item in brandStores)
            {
                item.IsDeleted = true;
            }
            foreach (var brandId in updateStoreDto.Brands)
            {
                brandStore=_context.BrandStore.FirstOrDefault(s=>s.StoreId==id&&s.BrandId==brandId);
                if(brandStore != null)
                {
                    brandStore.IsDeleted = false;
                }
                else
                {
                    brandStore = new()
                    {
                        BrandId= brandId,
                        StoreId=store.Id
                    };
                    _context.BrandStore.Add(brandStore);
                }
            }
            foreach (var item in storeCategories)
            {
                item.IsDeleted = true;
            }
            foreach (var categoryId in updateStoreDto.Categories)
            {
                storeCategory = _context.StoreCategory.FirstOrDefault(s=>s.StoreId==id&&s.CategoryId== categoryId);
                if(storeCategory != null)
                {
                    storeCategory.IsDeleted = false;
                }
                else
                {
                    storeCategory = new()
                    {
                        CategoryId= categoryId,
                        StoreId=store.Id
                    };
                    _context.StoreCategory.Add(storeCategory);
                }
            }
            _context.SaveChanges();
            return 202;
        }
    }
}
