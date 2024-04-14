using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.BrandDTOs;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.Models.ReletionTables;
using Microsoft.EntityFrameworkCore;

namespace ClothesSalePlatform.Services.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;

        public BrandService(AppDbContext context)
        {
            _context = context;
        }


        public ReturnBrandListDto GetAll(IMapper _mapper)
        {
            var brand = _context.Brand
                .Where(b => !b.IsDeleted)
                .Include(b=>b.Products)
                .Include(b => b.BrandCategory)
                .ThenInclude(b => b.Category)
                .Include(b => b.BrandStore)
                .ThenInclude(b => b.Store)
                .ToList();

            if (brand == null) return null;

            ReturnBrandListDto result = new()
            {
                BrandCount = brand.Count,
            };

            result.Values = _mapper.Map<List<ReturnBrandDto>>(brand);

            foreach (var value in result.Values)
            {

                foreach (var category in value.CategoriesInBrandDto)
                {
                    category.ProductCount=_context.Products.Where(c=>c.Category.Name==category.Name&&!c.IsDeleted&&c.Brand.Name==value.Name).Count();
                }

                foreach (var store in value.StoresInBrandDto)
                {
                    store.ProductCount=_context.Products.Where(c=>c.Store.Name== store.Name&&!c.IsDeleted&&c.Brand.Name==value.Name).Count();
                }


            }


            return result;
        }

        public ReturnBrandDto GetOne(int? id, IMapper _mapper)
        {
            
            var brand = _context.Brand.
                Where(b=>!b.IsDeleted)
                .Include(b => b.Products)
                .Include(b=>b.BrandCategory)
                .ThenInclude(b=>b.Category)
                .Include(b=>b.BrandStore)
                .ThenInclude(b=>b.Store)
                .FirstOrDefault(c => c.Id==id);
            if (brand == null) return null;
            var returnPrpductDto=_mapper.Map<ReturnBrandDto>(brand);

            foreach (var category in returnPrpductDto.CategoriesInBrandDto)
            {
                category.ProductCount = _context.Products.Where(c => c.Category.Name == category.Name && !c.IsDeleted && c.Brand.Name == returnPrpductDto.Name).Count();
            }

            foreach (var store in returnPrpductDto.StoresInBrandDto)
            {
                store.ProductCount = _context.Products.Where(c => c.Store.Name == store.Name && !c.IsDeleted && c.Brand.Name == returnPrpductDto.Name).Count();
            }

            return returnPrpductDto;
        }
        public int Create(CreateBrandDto createBrandDto, IMapper _mapper)
        {
            if (createBrandDto == null) return 400;
            var brand = _mapper.Map<Brand>(createBrandDto);
            Product product;
          
            foreach (var id in createBrandDto.Store)
            {
                BrandStore brandStore = new()
                {
                    Brand = brand,
                    StoreId = id
                };
                _context.BrandStore.Add(brandStore);

            }
            foreach (var id in createBrandDto.Category)
            {
                BrandCategory brandCategory = new()
                {
                    Brand = brand,
                   CategoryId = id
                };
                _context.BrandCategory.Add(brandCategory);

            }

            _context.SaveChanges();

           return 201;
        }

        public int Update(int?id,UpdateBrandDto updateBrandDto, IMapper _mapper)
        {
            if (updateBrandDto == null|| id == null) return 400;
            var brand = _context.Brand.Where(b=>!b.IsDeleted).FirstOrDefault(b=>b.Id==id);
            if (brand == null) return 404;
            
            _mapper.Map(updateBrandDto,brand);

            BrandCategory brandCategory;
            BrandStore brandStore;
            var bradnCategoryRel = _context.BrandCategory.Where(bc =>  bc.BrandId == id).ToList();
            var bradnStoreRel = _context.BrandStore.Where(bc =>  bc.BrandId == id).ToList();
            foreach (var category in bradnCategoryRel)
            {
                category.IsDeleted = true;
            }
            foreach (var store in bradnStoreRel)
            {
                store.IsDeleted = true;
            }
            foreach (var categoryId in updateBrandDto.Category)
            {
                brandCategory=_context.BrandCategory.FirstOrDefault(bc=>bc.CategoryId==categoryId&&bc.BrandId==id);
                if (brandCategory == null)
                {
                    brandCategory = new()
                    {
                        BrandId = brand.Id,
                        CategoryId=categoryId
                    };
                    _context.BrandCategory.Add(brandCategory);
                }
                else
                {
                    brandCategory.IsDeleted=false;
                }
            }
            foreach (var storeId in updateBrandDto.Store)
            {
                brandStore=_context.BrandStore.FirstOrDefault(bc=>bc.StoreId==storeId&&bc.BrandId==id);
                if (brandStore == null)
                {
                    brandStore = new()
                    {
                        BrandId = brand.Id,
                        StoreId = storeId
                    };
                    _context.BrandStore.Add(brandStore);
                }
                else
                {
                    brandStore.IsDeleted=false;
                }
            }
            _context.SaveChanges();


            return 202;
        }

        public int Delete(int? id)
        {
            if (id == null) return 400;
            var brand=_context.Brand.Where(b=>!b.IsDeleted).FirstOrDefault(b=>b.Id==id);
            if (brand == null) return 404;
            brand.DeletedAt = DateTime.Now;
            brand.IsDeleted = true;
            _context.SaveChanges();
            return 204;
        }
    }
}
