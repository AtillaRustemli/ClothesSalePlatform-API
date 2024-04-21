using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.EmailDTOs;
using ClothesSalePlatform.DTOs.ProductDTOs;
using ClothesSalePlatform.Helpers.Extensions;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.Models.ReletionTables;
using ClothesSalePlatform.Services.EmailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothesSalePlatform.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public ProductService(AppDbContext context, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }

        public ReturnProductListDto GetAll(int page, int take, string? search, IMapper _mapper)
        {
            var products = _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.ProductSize)
                .ThenInclude(p => p.Size)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Gender)
                .Include(p => p.Store)
                .ToList();
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();
            }

          
           
            var returnProductList = new ReturnProductListDto();
            returnProductList.TotalCount = products.Count;
            returnProductList.Items = _mapper.Map<List<ReturnProductDto>>(products);
            foreach (var product in returnProductList.Items)
            {
                foreach (var size in product.sizeInProductDTO)
                {
                    size.ProductCount = _context.ProductSize.Where(ps => !ps.IsDeleted && ps.Size.Name == size.Name&&!ps.Product.IsDeleted).ToList().Count;
                }


            }
            returnProductList.Items=returnProductList.Items
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();
            return returnProductList;
        }
        public int Create( CreateProductDto createProductDto, IMapper _mapper)
        {

            if (createProductDto == null) return 404;
            var product = _mapper.Map<Product>(createProductDto);
            if(createProductDto.ProductCount > 0)
                product.InStock= true;
            else
                product.InStock= false;
            int count = 0;
            _context.Products.Add(product);
            ProductSize prouctSize;
            foreach (var id in createProductDto.SizeId)
            {
                prouctSize = new()
                {
                    Product= product,
                    SizeId=id
                };
                _context.ProductSize.Add(prouctSize);
            }
            foreach (var photo in createProductDto.Photos)
            {
                count++;
                if (!photo.CheckImage("image/") && photo.CheckSize(1000)) return 400;
              

                var productImage = new ProductImage()
                {
                    ImgUrl = photo.SaveImage("wwwroot/img"),
                    Product = product
                };
                if (count ==1)
                {
                    productImage.IsMain = true;
                }
                else
                {
                    productImage.IsMain = false;
                }

                _context.ProductImages.Add(productImage);


            }
            #region Email To Brand
            CreateEmailDto createEmailToBrand = new()
            {
                ProductPrice = createProductDto.Price.ToString(),
                Subject =$"Create new product named {createProductDto.Name}",
                ProductName=createProductDto.Name,
                ProductColor=createProductDto.Color,
            };
            var brandSubscribers = _context.BrandSubscriber
                .Where(s => !s.IsDeleted && s.BrandId== createProductDto.BrandId)
                .Include(bs=>bs.Subscriber)
                .Include(s=>s.Brand)
                .ToList();
            List<string> addresses = new();
            foreach (var brandSubscriber in brandSubscribers)
            {
                addresses.Add(brandSubscriber.Subscriber.Email);
            }
            createEmailToBrand.Source = _context.Brand.Where(b => !b.IsDeleted).FirstOrDefault(b => b.Id == createProductDto.BrandId).Name;
            createEmailToBrand.Addresses = addresses;
            if (addresses != null)
            {
            var emailMessage=_emailService.CreateEmaill(createEmailToBrand);
            _emailService.SendEmaill(emailMessage);

            }
            #endregion
            #region Email To Category
            CreateEmailDto createEmailToCategory = new()
            {
                ProductPrice = createProductDto.Price.ToString(),
                Subject = $"Create new product named {createProductDto.Name}",
                ProductName = createProductDto.Name,
                ProductColor = createProductDto.Color,
            };
            var categorySubscribers = _context.CategorySubscriber
                .Where(s => !s.IsDeleted && s.CategoryId == createProductDto.CategoryId)
                .Include(bs => bs.Subscriber)
                .Include(s => s.Category)
                .ToList();
            List<string> addressesToCategory = new();
            foreach (var categorySubscriber in categorySubscribers)
            {
                addressesToCategory.Add(categorySubscriber.Subscriber.Email);
            }
            createEmailToCategory.Source = _context.Categories.Where(b => !b.IsDeleted).FirstOrDefault(b => b.Id == createProductDto.CategoryId).Name;
            createEmailToCategory.Addresses = addressesToCategory;
            if (addressesToCategory != null)
            {
                var emailMessage = _emailService.CreateEmaill(createEmailToCategory);
                _emailService.SendEmaill(emailMessage);

            }
            #endregion
            #region Email To Store
            CreateEmailDto createEmailToStore = new()
            {
                ProductPrice = createProductDto.Price.ToString(),
                Subject = $"Create new product named {createProductDto.Name}",
                ProductName = createProductDto.Name,
                ProductColor = createProductDto.Color,
            };
            var storeSubscribers = _context.StoreSubscriber
                .Where(s => !s.IsDeleted && s.StoreId == createProductDto.StoreId)
                .Include(bs => bs.Subscriber)
                .Include(s => s.Store)
                .ToList();
            List<string> addressesToStore = new();
            foreach (var storeSubscriber in storeSubscribers)
            {
                addressesToStore.Add(storeSubscriber.Subscriber.Email);
            }
            createEmailToStore.Source = _context.Store.Where(b => !b.IsDeleted).FirstOrDefault(b => b.Id == createProductDto.StoreId).Name;
            createEmailToStore.Addresses = addressesToStore;
            if (addressesToStore != null)
            {
                var emailMessage = _emailService.CreateEmaill(createEmailToStore);
                _emailService.SendEmaill(emailMessage);

            }
            #endregion
            _context.SaveChanges();
          

            return 201;
        }

        public int Delete(int? id)
        {

            if (id == null) return 400;
          var product=_context.Products.FirstOrDefault(p=>p.Id==id);
            if (product == null) return 404;
            product.IsDeleted = true;
            product.DeletedAt = DateTime.Now;
            _context.SaveChanges();
            return 204;
        }

        public ReturnProductDto Get(int? id, IMapper _mapper)
        {
            
            var product = _context.Products.
                 Where(p => !p.IsDeleted)
                 .Include(p => p.ProductSize)
                 .ThenInclude(p => p.Size)
                 .Include(p => p.Category)
                 .ThenInclude(pc => pc.Products)
                 .Include(p => p.Brand)
                 .ThenInclude(p => p.Products)
                 .Include(p => p.Store)
                 .ThenInclude(p => p.Products)
                 .Include(p => p.Gender)
                 .ThenInclude(p => p.Product)
                 .FirstOrDefault(p => p.Id == id);
            if (product == null) return null;

            var result = _mapper.Map<ReturnProductDto>(product);
            foreach (var size in result.sizeInProductDTO)
            {
                size.ProductCount = _context.ProductSize.Where(ps => !ps.IsDeleted&&ps.ProductId==id&&ps.Size.Name==size.Name).ToList().Count;
            }
            return result;
        }

        public int Update(UpdateProductDto udateProductDto, int?id,IMapper _mapper)
        {
            if (udateProductDto is null) return 400;
            if (id is null) return 400;
            var product=_context.Products.Where(p=>!p.IsDeleted).FirstOrDefault(p => p.Id == id);
            if (product is null) return 404;
            if (udateProductDto.ProductCount == 0) udateProductDto.InStock = false;
            else udateProductDto.InStock = true;
            _mapper.Map(udateProductDto, product);
            int count = 0;
            ProductSize productSize;
            var productSizes=_context.ProductSize.Where(ps=>ps.ProductId==id).ToList();
            foreach ( var size in productSizes)
            {
                size.IsDeleted = true;
            }
            foreach (var sizeId in udateProductDto.SizeId)
            {
                productSize = _context.ProductSize.FirstOrDefault(ps => ps.ProductId == id && ps.SizeId == sizeId);
                if (productSize == null)
                {
                    productSize = new()
                    {
                        ProductId = product.Id,
                        SizeId = sizeId
                    };
                    _context.ProductSize.Add(productSize);
                }
                else
                {
                    productSize.IsDeleted = false;
                }
            }
            foreach (var photo in udateProductDto.Photo)
            {

                if (!photo.CheckImage("image") && !photo.CheckSize(1000)) return 400;

                var productImage = new ProductImage()
                {
                    ImgUrl = photo.SaveImage("wwwroot/img"),
                    ProductId = product.Id
                };
                if (_context.ProductImages.Where(pi => pi.IsMain && !pi.IsDeleted) == null)
                {
                    productImage.IsMain = true;
                }
                else
                {
                    productImage.IsMain = false;
                }
                _context.ProductImages.Add(productImage);


            }


           



            _context.SaveChanges();

            return 202;
        }
    }
}
