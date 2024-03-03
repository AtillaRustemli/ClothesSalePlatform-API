using AutoMapper;
using ClothesSalePlatform.DTOs.ProductDTOs;

namespace ClothesSalePlatform.Services.ProductServices
{
    public interface IProductService
    {
         ReturnProductListDto GetAll(int page, int take, string? search, IMapper _mapper);
         ReturnProductListDto Get(int? id, IMapper _mapper);
         ReturnProductListDto Create(CreateProductDto createProductDto,IMapper _mapper);
         ReturnProductListDto Delete(int? id);
    }
}
