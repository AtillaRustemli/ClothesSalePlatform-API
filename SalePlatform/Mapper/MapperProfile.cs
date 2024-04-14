using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.AccountDTOs;
using ClothesSalePlatform.DTOs.BrandDTOs;
using ClothesSalePlatform.DTOs.CategoryDTOs;
using ClothesSalePlatform.DTOs.ProductDTOs;
using ClothesSalePlatform.DTOs.StoreDTOs;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.Models.ReletionTables;
using static ClothesSalePlatform.DTOs.ProductDTOs.ReturnProductDto;

namespace ClothesSalePlatform.Mapper
{
    public class MapperProfile:Profile
    {

        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public MapperProfile(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }   

        public MapperProfile()
        {
            CreateMap<AppUser, ReturnUserDto>();

            //------------------------------------------------------------------------------------------------------------------------
            //-----------------------------------------------Product------------------------------------------------------------------
            CreateMap<Product, ReturnProductDto>()
                .ForMember(d=>d.brandInProductDTO,map=>map.MapFrom(src=> new BrandInProductDTO
                {
                        Name=src.Brand.Name,
                        ProductCount=src.Brand.Products.Count,
                }))
                .ForMember(d=>d.sizeInProductDTO,map=>map.MapFrom(src=>src.Size))
                .ForMember(d => d.categoryInProductDTO, map => map.MapFrom(src => new CategoryInProductDTO
                {
                    Name = src.Category.Name,
                    ProductCount = src.Category.Products.Count,

                }))
                .ForMember(d=>d.genderInProductDTO,map=>map.MapFrom(src=>src.Gender))
                .ForMember(d=>d.storeInProductDTO,map=>map.MapFrom(src=> new StoreInProductDTO
                {
                    Name = src.Store.Name,
                    ProductCount = src.Store.Products.Count,
                }));
            //CreateMap<Category, CategoryInProductDTO>();
            CreateMap<Size, SizeInProductDTO>();
            CreateMap<Brand, BrandInProductDTO>();
            CreateMap<Gender, GenderInProductDTO>();
            //CreateMap<Store, StoreInProductDTO>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            //------------------------------------------------------------------------------------------------------------------------
            //-------------------------------------------------Category---------------------------------------------------------------
            CreateMap<Category, ReturnCategoryDto>()
                .ForMember(c => c.ProductCount, map => map.MapFrom(src => src.Products.Where(p => !p.IsDeleted).ToList().Count))
                .ForMember(d => d.BrandInCategoryDto, map => map.MapFrom(src => src.BrandCategory.Where(bc => !bc.IsDeleted).Select(s => s.Brand).ToArray()))
                .ForMember(d => d.StoreInCategoryDto, map => map.MapFrom(src => src.StoreCategory.Where(sc => !sc.IsDeleted).Select(s => s.Store).ToArray()))
                ;
            CreateMap<Store, StoreInCategoryDto>();
            CreateMap<Brand, BrandInCategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            //------------------------------------------------------------------------------------------------------------------------
            //---------------------------------------------------Brand----------------------------------------------------------------
            CreateMap<Brand, ReturnBrandDto>()
                .ForMember(b=>b.ProductCount,map=>map.MapFrom(src=>src.Products.Where(p => !p.IsDeleted).ToList().Count))
               .ForMember(c=>c.CategoriesInBrandDto,map=>map.MapFrom(src=>src.BrandCategory.Where(bc => !bc.IsDeleted).Select(s=>s.Category)))
               .ForMember(c=>c.StoresInBrandDto,map=>map.MapFrom(src=>src.BrandStore.Where(bs => !bs.IsDeleted).Select(s => s.Store).ToArray()));
           
            CreateMap<Category,CategoryInBrandDto>()
                .ForMember(d=>d.ProductCount,map=>map.MapFrom(src=>src.Products.Count))
                .ForMember(d=>d.Name,map=>map.MapFrom(src=>src.Name));
            CreateMap<Store,StoreInBrandDto>()
                .ForMember(d => d.ProductCount, map => map.MapFrom(src => src.Products.Count));

            CreateMap<CreateBrandDto, Brand>();
            CreateMap<UpdateBrandDto, Brand>();
            //------------------------------------------------------------------------------------------------------------------------
            //---------------------------------------------------Store----------------------------------------------------------------
            CreateMap<Store, ReturnStoreDto>()
                .ForMember(s=>s.ProductCount,map=>map.MapFrom(src=>src.Products.Where(sc => !sc.IsDeleted).ToList().Count))
                .ForMember(d=>d.CategoryInStoreDto,map=>map.MapFrom(src=>src.StoreCategory.Where(sc=>!sc.IsDeleted).Select(s=>s.Category)))
                .ForMember(d=>d.BrandInStoreDto,map=>map.MapFrom(src=>src.BrandStore.Where(bs => !bs.IsDeleted).Select(s=>s.Brand)))
                ;
            CreateMap<Category, CategoryInStoreDto>();
            CreateMap<Brand, BrandInStoreDto>();


        }
    }
}
