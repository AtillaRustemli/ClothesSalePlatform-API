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
            
            CreateMap<Category, CategoryInProductDTO>();
            //CreateMap<AppUser, ReturnUserDto>();
            CreateMap<Product, ReturnProductDto>();
            CreateMap<Product, ReturnProductDto>();
        }
    }
}
