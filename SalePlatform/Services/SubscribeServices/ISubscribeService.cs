using AutoMapper;
using ClothesSalePlatform.DTOs.SubscriberDTOs;
using System.Security.Claims;
using System.Security.Principal;

namespace ClothesSalePlatform.Services.SubscribeServices
{
    public interface ISubscribeService
    {
        ReturnSubscriberListDto GetAll(IMapper mapper);
        ReturnSubscriberDto GetOne(int?id,string?name,IMapper mapper);
        int SubscribeBrand(int? brandId,ClaimsPrincipal user);
        int UnsubscribeBrand(int? brandId,ClaimsPrincipal user);
        int SubscribeCategory(int? categoryId,ClaimsPrincipal user);
        int UnsubscribeCategory(int? categoryId, ClaimsPrincipal user);
        int SubscribeStore(int? storeId,ClaimsPrincipal user);
        int UnsubscribeStore(int? storeId, ClaimsPrincipal user);
    }
}
