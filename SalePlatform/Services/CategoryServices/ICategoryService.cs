﻿using AutoMapper;
using ClothesSalePlatform.DTOs.CategoryDTOs;

namespace ClothesSalePlatform.Services.CategoryServices
{
    public interface ICategoryService
    {
        ReturnCategoryListDto GetAll(IMapper mapper);
        ReturnCategoryDto GetOne(IMapper mapper,int?id);
        int Create(CreateCategoryDto createCategoryDto,IMapper mapper);
        int Update();
        int Delete(int?id);

    }
}
