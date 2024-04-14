using AutoMapper;
using ClothesSalePlatform.DTOs.StoreDTOs;

namespace ClothesSalePlatform.Services.StoreServices
{
    public interface IStoreService
    {
        ReturnStoreDto GetOne(int? id, IMapper mapper);
        ReturnStoreListDto GetAll(IMapper mapper);
        //int Create(int? id, IMapper mapper);
        //int Update(int? id, IMapper mapper);
        //int Delete(int? id, IMapper mapper);
    }
}
