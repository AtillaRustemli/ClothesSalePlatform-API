using AutoMapper;
using ClothesSalePlatform.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;

namespace ClothesSalePlatform.Services.ProductServices
{
    public interface IProductService
    {
         ReturnProductListDto GetAll(int page, int take, string? search, IMapper _mapper);
         ReturnProductDto Get(int? id, IMapper _mapper);
         int Create([FromForm] CreateProductDto createProductDto,IMapper _mapper);
         int Update([FromForm] UpdateProductDto udateProductDto,int?id, IMapper _mapper);
         int Delete(int? id);
    }
}
