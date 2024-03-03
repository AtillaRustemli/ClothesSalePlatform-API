using AutoMapper;
using ClothesSalePlatform.DTOs.ProductDTOs;
using ClothesSalePlatform.Models;
using static ClothesSalePlatform.DTOs.ProductDTOs.ReturnProductDto;

namespace ClothesSalePlatform.Mapper
{
    public class MapperProfile:Profile
    {

        private readonly IConfiguration _configuration;

        public MapperProfile(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MapperProfile()
        {
            //CreateMap<AppUser, ReturnUserDto>();
            
            CreateMap<Product, ReturnProductDto>()
                .ForMember(d=>d.brandInProductDTO,map=>map.MapFrom(src=>src.Brand))
                .ForMember(d=>d.sizeInProductDTO,map=>map.MapFrom(src=>src.Size))
                .ForMember(d => d.categoryInProductDTO, map => map.MapFrom(src => src.Category))
                .ForMember(d=>d.genderInProductDTO,map=>map.MapFrom(src=>src.Gender))
                .ForMember(d=>d.storeInProductDTO,map=>map.MapFrom(src=>src.Store))
                ;
            CreateMap<Category, CategoryInProductDTO>();
            CreateMap<Size, SizeInProductDTO>();
            CreateMap<Brand, BrandInProductDTO>();
            CreateMap<Gender, GenderInProductDTO>();
            CreateMap<Store, StoreInProductDTO>();
        }
    }
}
