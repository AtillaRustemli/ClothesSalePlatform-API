using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.CategoryDTOs;
using ClothesSalePlatform.DTOs.ProductDTOs;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.Models.ReletionTables;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.WebSockets;

namespace ClothesSalePlatform.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public int Create(CreateCategoryDto createCategoryDto, IMapper _mapper)
        {

          var category=_context.Categories.FirstOrDefault(c=>c.Name==createCategoryDto.Name);
            if (category != null) return 400;
            category = _mapper.Map<Category>(createCategoryDto);
            List<int> brandList = new();
            List<int> storeList = new();
            _context.Categories.Add(category);
            for (int i = 0;i<createCategoryDto.Brand.Length;i++)
            {
                if(_context.Brand.Any(bc=>bc.Id== createCategoryDto.Brand[i]))
                {
                    if (brandList.Contains(createCategoryDto.Brand[i])) continue;

                    brandList.Add(createCategoryDto.Brand[i]);

                var brandCategory = new BrandCategory();
                brandCategory.BrandId = createCategoryDto.Brand[i];
                    brandCategory.Category = category;
                    _context.BrandCategory.Add(brandCategory);

                }
            }
            for (int i = 0;i<createCategoryDto.Store.Length;i++)
            {
                if(_context.Store.Any(bc=>bc.Id== createCategoryDto.Store[i]))
                {
                    if (storeList.Contains(createCategoryDto.Store[i])) continue;

                    storeList.Add(createCategoryDto.Store[i]);

                var storeCategory = new StoreCategory();
                storeCategory.StoreId = createCategoryDto.Store[i];
                storeCategory.Category = category;
                    _context.StoreCategory.Add(storeCategory);

                }
            }


            _context.SaveChanges();
            return 201;
        }

        public int Delete(int? id)
        {
            if (id == null) return 400;
            var category=_context.Categories.Where(c=>!c.IsDeleted).FirstOrDefault(c => c.Id == id);
            if (category == null) return 404;

            category.IsDeleted=true;
            category.DeletedAt= DateTime.Now;
            _context.SaveChanges();

            return 204;
        }

        public ReturnCategoryListDto GetAll(IMapper _mapper)
        {
            var categories=_context.Categories
                .Where(p=>!p.IsDeleted)
                .Include(p=>p.BrandCategory)
                .ThenInclude(p=>p.Brand)
                .Include(p=>p.StoreCategory)
                .ThenInclude(p=>p.Store)
                .ToList();

            if (categories == null) return null;
            var result=new ReturnCategoryListDto();
            result.Values = _mapper.Map<List<ReturnCategoryDto>>(categories);
            int id;

            for (int i = 0; i < categories.Count; i++)
            {
            for (var j = 0; j < result.Values[i].BrandInCategoryDto.Length; j++)
            {
                    result.Values[i].BrandInCategoryDto[j].CategoryProductCount = _context.Products.Where(p => p.CategoryId == categories[i].Id && p.Brand.Name == result.Values[i].BrandInCategoryDto[j].Name).Count();
            }
            for (var j = 0; j < result.Values[i].StoreInCategoryDto.Length; j++)
            {
                result.Values[i].StoreInCategoryDto[j].CategoryProductCount = _context.Products.Where(p => p.CategoryId == categories[i].Id && p.Store.Name == result.Values[i].StoreInCategoryDto[j].Name).Count();
            }

            }


            result.CategoryCount=categories.Count();




            return result;
        }

        public ReturnCategoryDto GetOne(IMapper _mapper, int? id)
        {
       
            var product = _context.Categories
                .Where(c => !c.IsDeleted)
                .Include(c => c.BrandCategory)
                .ThenInclude(c => c.Brand)
                .Include(c => c.StoreCategory)
                .ThenInclude(c => c.Store)
                .FirstOrDefault(x => x.Id == id);

            if (product == null) return null;

            var result = _mapper.Map<ReturnCategoryDto>(product);
            for (var i = 0; i < result.BrandInCategoryDto.Length; i++)
            {
                result.BrandInCategoryDto[i].CategoryProductCount = _context.Products.Where(p => p.CategoryId == id && p.Brand.Name == result.BrandInCategoryDto[i].Name).Count();
            }
            for (var i = 0; i < result.StoreInCategoryDto.Length; i++)
            {
                result.StoreInCategoryDto[i].CategoryProductCount = _context.Products.Where(p => p.CategoryId == id && p.Store.Name == result.StoreInCategoryDto[i].Name).Count();
            }
            return result;
        }

        public int Update(int? id, UpdateCategoryDto updateCategoryDto, IMapper _mapper)
        {
            if(id == null) return 400;
            if(updateCategoryDto == null) return 400;
            Store store;
            //foreach (var item in updateCategoryDto.Store)
            //{
            //    store= _context.Store.FirstOrDefault(store=>store.Id == item);
            //}
            var category=_context.Categories
                .Where(c=>!c.IsDeleted)
                .FirstOrDefault(c => c.Id == id);
            category.Name=updateCategoryDto.Name;
            category.Products = null;
            category.BrandCategory = null;
            category.StoreCategory = null;
             BrandCategory brandCategory;
             StoreCategory storeCategory;
            foreach (var item in updateCategoryDto.Products)
            {
                category.Products.Add(_context.Products.FirstOrDefault(c => c.Id == item));
            }
            foreach (var item in updateCategoryDto.Brand)
            {
                brandCategory = new()
                {
                    BrandId=item,
                    CategoryId=category.Id,
                };
                
                category.BrandCategory.Add(brandCategory);
            }
            foreach (var item in updateCategoryDto.Products)
            {
                storeCategory = new()
                {
                    StoreId = item,
                    CategoryId = category.Id,
                };  
                category.StoreCategory.Add(storeCategory);
            }
            _context.SaveChanges();

            if (category == null) return 404;


            return 202;

        }
    }
}
