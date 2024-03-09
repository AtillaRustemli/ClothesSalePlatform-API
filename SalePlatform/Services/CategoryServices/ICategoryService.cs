using AutoMapper;
using ClothesSalePlatform.DTOs.CategoryDTOs;

namespace ClothesSalePlatform.Services.CategoryServices
{
    public interface ICategoryService
    {
        ReturnCategoryDto GetOne(IMapper mapper,int?id);
        ReturnCategoryDto GetAll(IMapper mapper,int?id);
        int Create();
        int Update();
        int Delete();

    }
}
