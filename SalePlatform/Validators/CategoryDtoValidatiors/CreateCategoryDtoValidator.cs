using ClothesSalePlatform.DTOs.CategoryDTOs;
using FluentValidation;

namespace ClothesSalePlatform.Validators.CategoryDtoValidatiors
{
    public class CreateCategoryDtoValidator:AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(c=>c.Name)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaq!");
        }
    }
}
