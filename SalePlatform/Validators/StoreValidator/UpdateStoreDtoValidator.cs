using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.StoreDTOs;
using FluentValidation;

namespace ClothesSalePlatform.Validators.StoreValidator
{
    public class UpdateStoreDtoValidator:AbstractValidator<UpdateStoreDto>
    {
        private readonly AppDbContext _context;

        public UpdateStoreDtoValidator(AppDbContext context)
        {
            _context = context;
            RuleFor(s => s.Name)
             .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!")
             ;
            RuleFor(s => s.Description)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(s => s.ProductCount)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(s => s.ClosingHours)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(s => s.OpeningHours)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(s => s.ProductCount)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(s => s.ProductCount)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(s => s.Brands)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!")
                .Custom((s, custom) =>
                {
                    foreach (var id in s)
                    {
                        if (!_context.Brand.Where(s => !s.IsDeleted).Any(b => b.Id == id))
                        {
                            custom.AddFailure("Brands", "Bele brand movcud deyil!");
                        }
                    }
                });
            RuleFor(s => s.Categories)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!")
                .Custom((s, custom) =>
                {
                    foreach (var id in s)
                    {
                        if (!_context.Categories.Where(s => !s.IsDeleted).Any(b => b.Id == id))
                        {
                            custom.AddFailure("Categories", "Bele kateqoriya movcud deyil!");
                        }
                    }
                });

          

        }
    }
}
