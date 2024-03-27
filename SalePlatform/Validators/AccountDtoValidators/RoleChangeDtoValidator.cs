using ClothesSalePlatform.DTOs.AccountDTOs;
using FluentValidation;

namespace ClothesSalePlatform.Validators.AccountDtoValidators
{
    public class RoleChangeDtoValidator:AbstractValidator<RoleChangeDto>
    {
        public RoleChangeDtoValidator()
        {
            RuleFor(r=>r.Roles)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(r=>r.UserName)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
        }


    }
}
