using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.StoreDTOs;
using FluentValidation;

namespace ClothesSalePlatform.Validators.StoreValidator
{
    public class CreateStoreDtoValidator:AbstractValidator<CreateStoreDto>
    {
        private readonly AppDbContext _context;

        public CreateStoreDtoValidator(AppDbContext context)
        {
            _context = context;
            RuleFor(s=>s.Name)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!")
                .Custom((s, custom) =>
                {
                    if (_context.Store.Where(st => !st.IsDeleted).Any(st => st.Name == s))
                        custom.AddFailure("Name","Bu adli magaza movcuddur!");
                })
                ;
            RuleFor(s=>s.Description)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(s=>s.ProductCount)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(s=>s.ClosingHours)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(s=>s.OpeningHours)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(s=>s.ProductCount)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(s=>s.ProductCount)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(s => s.Brands)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!")
                .Custom((s, custom) =>
                {
                    foreach (var id in s)
                    {
                        if(!_context.Brand.Where(s => !s.IsDeleted).Any(b => b.Id == id))
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
                        if(!_context.Categories.Where(s=>!s.IsDeleted).Any(b => b.Id == id))
                        {
                            custom.AddFailure("Categories", "Bele kateqoriya movcud deyil!");
                        }
                    }
                });
        }

    }
}
