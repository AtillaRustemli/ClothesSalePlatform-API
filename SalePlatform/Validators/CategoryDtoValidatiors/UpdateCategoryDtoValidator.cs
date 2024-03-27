using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.CategoryDTOs;
using ClothesSalePlatform.Models;
using FluentValidation;

namespace ClothesSalePlatform.Validators.CategoryDtoValidatiors
{
    public class UpdateCategoryDtoValidator:AbstractValidator<UpdateCategoryDto>
    {
        private readonly AppDbContext _context;

        public UpdateCategoryDtoValidator(AppDbContext context)
        {
            _context = context;
            RuleFor(c => c.Name)
              .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(c => c.Brand)
              .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(c => c.Products)
              .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(c => c.Store)
              .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(c => c).Custom((c, custom) =>
            {

                foreach (var item in c.Store)
                {
                    if (_context.Store.FirstOrDefault(c => c.Id == item) == null) custom.AddFailure("Store", "Bele magaza yoxdur");

                }
                foreach (var item in c.Brand)
                {
                    if (_context.Brand.FirstOrDefault(c => c.Id == item) == null) custom.AddFailure("Brand", "Bele brend yoxdur");

                }
                foreach (var item in c.Products)
                {
                    if (_context.Products.FirstOrDefault(c => c.Id == item) == null) custom.AddFailure("Products", "Bele mehsul yoxdur");

                }

            });
        }

    }
}
