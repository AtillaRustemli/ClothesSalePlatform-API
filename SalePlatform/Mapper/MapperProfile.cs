using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.AccountDTOs;
using ClothesSalePlatform.DTOs.CategoryDTOs;
using ClothesSalePlatform.DTOs.ProductDTOs;
using ClothesSalePlatform.Models;
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
                .ForMember(c => c.ProductCount, map => map.MapFrom(src => src.Products.Count))
                .ForMember(d => d.BrandInCategoryDto, map => map.MapFrom(src => src.BrandCategory.Select(s => s.Brand)))
                .ForMember(d => d.StoreInCategoryDto, map => map.MapFrom(src => src.StoreCategory.Select(s => s.Store)))
                ;
            CreateMap<Brand, BrandInCategoryDto>();
            CreateMap<Store, StoreInCategoryDto>();
            CreateMap<CreateCategoryDto, Category>();


        }
    }
}
