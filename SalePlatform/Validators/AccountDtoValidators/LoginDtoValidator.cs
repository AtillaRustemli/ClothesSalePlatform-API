using ClothesSalePlatform.DTOs.AccountDTOs;
using FluentValidation;

namespace ClothesSalePlatform.Validators.AccountDtoValidators
{
    public class LoginDtoValidator:AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(l => l.UserNameOrEmail)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!")
                .MaximumLength(50).WithMessage("Chox uzundur");
            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!")
                .MinimumLength(8).WithMessage("Simvol sayi 8-den ashagi olmamalidir!");
        }
    }
}
