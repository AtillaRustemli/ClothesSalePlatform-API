using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.ProductDTOs;
using FluentValidation;

namespace ClothesSalePlatform.Validators.ProductValidators
{
    public class CreateProductDtoValidator:AbstractValidator<CreateProductDto>
    {
        private readonly AppDbContext _context;

        public CreateProductDtoValidator(AppDbContext context)
        {
            _context = context;
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p => p.Color)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p => p.Price)
                //.Must(BeInteger).WithMessage("Eded Olmalidir")
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!")
                .Must(p => p >= 0).WithMessage("Mehsulun qiymeti 0-dan ashagi ola bilmez!!!");
            RuleFor(p => p.ProductCount)
                .Must(p => p >= 0).WithMessage("Mehsul sayi 0-dan ashagi ola bilmez!!!");
            //    .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");-->ProductCount 0-a beraber ola biler!
            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!")
                .Custom((p, custom) =>
                {
                if (_context.Categories.Where(c => !c.IsDeleted).FirstOrDefault(c => c.Id == p) == null){
                        custom.AddFailure("CategoryId", "Bele bir kateqoriya yoxdur");
            }
                });
            RuleFor(p => p.SizeId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!")
                .Custom((p, custom) =>
                {
                    if (p != null)
                    {
                    foreach (var id in p)
                    {
                    if (_context.Size.Where(s => !s.IsDeleted ).FirstOrDefault(s => s.Id == id) == null)  custom.AddFailure("SizeId", "Bele olcu yoxdur!!!!");
                    }

                    }
                });
            RuleFor(p => p.BrandId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!")
                .Custom((p, custom) =>
                {
                    if (_context.Brand.Where(c => !c.IsDeleted).FirstOrDefault(c => c.Id == p) == null)
                    {
                        custom.AddFailure("BrandId", "Bele bir brand yoxdur");
                    }
                });
            RuleFor(p => p.GenderId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!")
                .Custom((p, custom) =>
                {
                    if (_context.Gender.Where(c => !c.IsDeleted).FirstOrDefault(c => c.Id == p) == null)
                    {
                        custom.AddFailure("GenderId", "Bele bir cins yoxdur");
                    }
                });
            //RuleForEach(p => new string[]{ p.Name, p.Color})
            //.NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            //RuleForEach(p => new int[]{ p.ProductCount, p.CategoryId, p.SizeId, p.StoreId, p.BrandId, p.GenderId })
            //.NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            //RuleFor(p=> p.Price)
            //.NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
        }



    }
}
