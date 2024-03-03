using ClothesSalePlatform.DTOs.ProductDTOs;
using FluentValidation;

namespace ClothesSalePlatform.Validators.ProductValidators
{
    public class CreateProductDtoValidator:AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(p=>p.Name)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p=>p.Color)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p=>p.Price)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p=>p.ProductCount)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p=>p.CategoryId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p=>p.SizeId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p=>p.StoreId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p=>p.BrandId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p=>p.GenderId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
        }
    }
}
