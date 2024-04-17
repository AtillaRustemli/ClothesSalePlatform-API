using AutoMapper;
using ClothesSalePlatform.DTOs.StoreDTOs;

namespace ClothesSalePlatform.Services.StoreServices
{
    public interface IStoreService
    {
        ReturnStoreDto GetOne(int? id, IMapper mapper);
        ReturnStoreListDto GetAll(IMapper mapper);
        int Create(CreateStoreDto createStoreDto, IMapper mapper);
        int Update(int?id,UpdateStoreDto updateStoreDto, IMapper mapper);
        int Delete(int? id);
    }
}
