using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.SubscriberDTOs;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.Models.ReletionTables;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace ClothesSalePlatform.Services.SubscribeServices
{


    public class SubscribeService : ISubscribeService
    {
        private readonly AppDbContext _context;

        public SubscribeService(AppDbContext context)
        {
            _context = context;
        }

        public ReturnSubscriberListDto GetAll(IMapper _mapper)
        {
            var subscribers = _context.Subscribers
                .Where(s=>!s.IsDeleted)
                .Include(s=>s.BrandSubscriber)
                .ThenInclude(s=>s.Brand)
                .Include(s=>s.CategorySubscriber)
                .ThenInclude(s=>s.Category)
                .Include(s=>s.StoreSubscriber)
                .ThenInclude(s=>s.Store)
                .ToList();
            if (subscribers == null) return null;
            ReturnSubscriberListDto returnSubscriberListDto = new()
            {
               SubscriberCount= subscribers.Count
            };

            returnSubscriberListDto.Values = _mapper.Map<List<ReturnSubscriberDto>>(subscribers);

            foreach (var subscriber in returnSubscriberListDto.Values)
            {
                foreach (var brand in subscriber.BrandInSubscriberDto)
                {
                    brand.ProductCount = _context.Products.Where(s => !s.IsDeleted && s.Brand.Name == brand.Name).ToList().Count;
                }
                foreach (var category in subscriber.CategoryInSubscriberDto)
                {
                    category.ProductCount = _context.Products.Where(s => !s.IsDeleted && s.Category.Name == category.Name).ToList().Count;
                }
                foreach (var store in subscriber.StoreInSubscriberDto)
                {
                    store.ProductCount = _context.Products.Where(s => !s.IsDeleted && s.Store.Name == store.Name).ToList().Count;
                }
            }


            return returnSubscriberListDto;
        }

        public ReturnSubscriberDto GetOne(int? id, string? name, IMapper _mapper)
        {
            Subscriber subscriber;
            var query = _context.Subscribers
                .Where(s => !s.IsDeleted)
                .Include(s => s.BrandSubscriber)
                .ThenInclude(s => s.Brand)
                .Include(s => s.CategorySubscriber)
                .ThenInclude(s => s.Category)
                .Include(s => s.StoreSubscriber)
                .ThenInclude(s => s.Store);
                
            if (id != null)
            {
                subscriber = query.FirstOrDefault(s => s.Id == id);
            }
            else
            {
                return null;
            }
            if (subscriber == null) return null;
            var result=_mapper.Map<ReturnSubscriberDto>(subscriber);

            foreach(var brand in result.BrandInSubscriberDto)
                {
                brand.ProductCount = _context.Products.Where(s => !s.IsDeleted && s.Brand.Name == brand.Name).ToList().Count;
            }
            foreach (var category in result.CategoryInSubscriberDto)
            {
                category.ProductCount = _context.Products.Where(s => !s.IsDeleted && s.Category.Name == category.Name).ToList().Count;
            }
            foreach (var store in result.StoreInSubscriberDto)
            {
                store.ProductCount = _context.Products.Where(s => !s.IsDeleted && s.Store.Name == store.Name).ToList().Count;
            }
            return result;
        }

        public int SubscribeBrand(int? brandId, ClaimsPrincipal user)
        {
            var subscriber = _context.Subscribers
                .Where(s => !s.IsDeleted)
                .Include(s=>s.BrandSubscriber)
                .ThenInclude(s=>s.Brand)
                .FirstOrDefault(s => s.Email == user.FindFirstValue(ClaimTypes.Email));
            if (subscriber==null)
            {
                subscriber = new()
                {
                    Email = user.FindFirstValue(ClaimTypes.Email),
                };
                _context.Subscribers.Add(subscriber);
            }
            if (brandId == null) return 400;
            var brand=_context.Brand.Where(b=>!b.IsDeleted).FirstOrDefault(b=>b.Id==brandId);
            if (brand == null) return 404;
            BrandSubscriber brandSubscriber = new()
            {
                BrandId = brand.Id,
                SubscriberId=subscriber.Id,
            };
            var checkSubscriber = _context.BrandSubscriber.FirstOrDefault(s => s.BrandId == brandId && s.SubscriberId == subscriber.Id);
          if (checkSubscriber == null)
            {
          if (subscriber.BrandSubscriber == null)
            {
                subscriber.BrandSubscriber = new()
                {
                    brandSubscriber
                };
             
            }
            else
            {
                subscriber.BrandSubscriber.Add(brandSubscriber);
            }

            }
            else
            {
                if(checkSubscriber.IsDeleted) checkSubscriber.IsDeleted = false;
                else return 409; 
            }
            _context.SaveChanges();
            return 201;
        }

        public int SubscribeCategory(int? categoryId, ClaimsPrincipal user)
        {
            var subscriber = _context.Subscribers
                .Where(s => !s.IsDeleted)
                .Include(s => s.CategorySubscriber)
                .ThenInclude(s => s.Category)
                .FirstOrDefault(s => s.Email == user.FindFirstValue(ClaimTypes.Email));
            if (subscriber == null)
            {
                subscriber = new()
                {
                    Email = user.FindFirstValue(ClaimTypes.Email),
                };
                _context.Subscribers.Add(subscriber);
            }
            if (categoryId == null) return 400;
            var category = _context.Categories.Where(b => !b.IsDeleted).FirstOrDefault(b => b.Id == categoryId);
            if (category == null) return 404;
            CategorySubscriber categorySubscriber = new()
            {
                CategoryId = category.Id,
                SubscriberId = subscriber.Id,
            };
            var checkSubscriber = _context.CategorySubscriber.FirstOrDefault(s => s.CategoryId == categoryId && s.SubscriberId == subscriber.Id);
            if (checkSubscriber == null)
            {
                if (subscriber.CategorySubscriber == null)
                {
                    subscriber.CategorySubscriber = new()
                    {
                     categorySubscriber
                    };

                }
                else
                {
                    subscriber.CategorySubscriber.Add(categorySubscriber);
                }

            }
            else
            {
                if (checkSubscriber.IsDeleted) checkSubscriber.IsDeleted = false;
                else return 409;
            }
            _context.SaveChanges();
            return 201;
        }

        public int SubscribeStore(int? storeId, ClaimsPrincipal user)
        {
            var subscriber = _context.Subscribers
                .Where(s => !s.IsDeleted)
                .Include(s => s.StoreSubscriber)
                .ThenInclude(s => s.Store)
                .FirstOrDefault(s => s.Email == user.FindFirstValue(ClaimTypes.Email));
            if (subscriber == null)
            {
                subscriber = new()
                {
                    Email = user.FindFirstValue(ClaimTypes.Email),
                };
                _context.Subscribers.Add(subscriber);
            }
            if (storeId == null) return 400;
            var store = _context.Store.Where(b => !b.IsDeleted).FirstOrDefault(b => b.Id == storeId);
            if (store == null) return 404;
            StoreSubscriber storeSubscriber = new()
            {
                StoreId = store.Id,
                SubscriberId = subscriber.Id,
            };
            var checkSubscriber = _context.StoreSubscriber.FirstOrDefault(s => s.StoreId == storeId && s.SubscriberId == subscriber.Id);
            if (checkSubscriber == null)
            {
                if (subscriber.StoreSubscriber == null)
                {
                    subscriber.StoreSubscriber = new()
                    {
                     storeSubscriber
                    };

                }
                else
                {
                    subscriber.StoreSubscriber.Add(storeSubscriber);
                }

            }
            else
            {
                if (checkSubscriber.IsDeleted) checkSubscriber.IsDeleted = false;
                else return 409;
            }
            _context.SaveChanges();
            return 201;
        }

        public int UnsubscribeBrand(int? brandId, ClaimsPrincipal user)
        {
            if (brandId == null) return 400;
            var bransSunscribe=_context.BrandSubscriber.Where(bs=>!bs.IsDeleted).FirstOrDefault(bs=>bs.BrandId==brandId&&bs.Subscriber.Email==user.FindFirstValue(ClaimTypes.Email));
            if (bransSunscribe == null) return 404;
            bransSunscribe.IsDeleted = true;
            bransSunscribe.DeletedAt = DateTime.Now;
            _context.SaveChanges();


            return 204;
        }

        public int UnsubscribeCategory(int? categoryId, ClaimsPrincipal user)
        {
            if (categoryId == null) return 400;
            var categorySunscribe = _context.CategorySubscriber.Where(bs => !bs.IsDeleted).FirstOrDefault(bs => bs.CategoryId == categoryId && bs.Subscriber.Email == user.FindFirstValue(ClaimTypes.Email));
            if (categorySunscribe == null) return 404;
            categorySunscribe.IsDeleted = true;
            categorySunscribe.DeletedAt = DateTime.Now;
            _context.SaveChanges();
            return 204;
        }

        public int UnsubscribeStore(int? storeId, ClaimsPrincipal user)
        {
            if (storeId == null) return 400;
            var storeSunscribe = _context.StoreSubscriber.Where(bs => !bs.IsDeleted).FirstOrDefault(bs => bs.StoreId == storeId && bs.Subscriber.Email == user.FindFirstValue(ClaimTypes.Email));
            if (storeSunscribe == null) return 404;
            storeSunscribe.IsDeleted = true;
            storeSunscribe.DeletedAt = DateTime.Now;
            _context.SaveChanges();
            return 204;
        }
    }
}
