using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.BrandDTOs;
using FluentValidation;

namespace ClothesSalePlatform.Validators.BrandDtoValidators
{
    public class UpdateBrandDtoValidator:AbstractValidator<UpdateBrandDto>
    {
        private readonly AppDbContext _context;
        public UpdateBrandDtoValidator(AppDbContext context)
        {
            _context = context;
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!");
            RuleFor(b => b.FoundedYear)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!");
            RuleFor(b => b.FoundedCountry)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!");
            RuleFor(b => b.Description)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!");
            RuleFor(b => b.Store)
                .Custom((b, custom) =>
                {
                    foreach (var id in b)
                    {
                        if (!_context.Store.Where(s => !s.IsDeleted).Any(s => s.Id == id))
                        {
                            custom.AddFailure("Store", "Bele maqaza movcud deyil!");
                        }
                    }
                });
            RuleFor(b => b.Category)
                .NotEmpty().WithMessage("Bosh saxlamqa olmaz")
                .Custom((b, custom) =>
                {
                    foreach (var id in b)
                    {
                        if (_context.Categories.Where(s => !s.IsDeleted).FirstOrDefault(b => b.Id == id) == null)
                        {
                            custom.AddFailure("Category", "Bele kateqoriya movcud deyil!");
                        }
                    }
                });
        }
    }
}
