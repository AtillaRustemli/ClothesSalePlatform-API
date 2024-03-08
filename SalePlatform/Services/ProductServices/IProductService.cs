using AutoMapper;
using ClothesSalePlatform.DTOs.ProductDTOs;

namespace ClothesSalePlatform.Services.ProductServices
{
    public interface IProductService
    {
         ReturnProductListDto GetAll(int page, int take, string? search, IMapper _mapper);
         ReturnProductDto Get(int? id, IMapper _mapper);
         int Create(CreateProductDto createProductDto,IMapper _mapper);
         int Update(UpdateProductDto udateProductDto,int?id, IMapper _mapper);
         int Delete(int? id);
    }
}
