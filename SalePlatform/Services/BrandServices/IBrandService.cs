using AutoMapper;
using ClothesSalePlatform.DTOs.BrandDTOs;

namespace ClothesSalePlatform.Services.BrandServices
{
    public interface IBrandService
    {
        ReturnBrandListDto GetAll(IMapper mapper);
        ReturnBrandDto GetOne(int?id,IMapper mapper);
        int Create(CreateBrandDto createBrandDto,IMapper mapper);
        int Update(int?id,UpdateBrandDto updateBrandDto, IMapper mapper);
        int Delete(int?id);
    }
}
