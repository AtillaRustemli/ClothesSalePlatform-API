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
                    if (_context.Store.Where(c=>!c.IsDeleted).FirstOrDefault(c => c.Id == item) == null) custom.AddFailure("Store", "Bele magaza yoxdur");

                }

            });
            RuleFor(c => c).Custom((c, custom) =>
            {
                Product product;
                foreach (var item in c.Products)
                {
                    product = _context.Products.FirstOrDefault(c => c.Id == item);
                    if (product == null || (product!=null && product.IsDeleted)) custom.AddFailure("Product", "Bele mehsul yoxdur");

                }

            });
            RuleFor(c => c).Custom((c, custom) =>
            {

             
                foreach (var item in c.Brand)
                {
                    if (_context.Brand.Where(c=>!c.IsDeleted).FirstOrDefault(c => c.Id == item) == null) custom.AddFailure("Brand", "Bele brend yoxdur");

                }
             

            });
        }

    }
}
