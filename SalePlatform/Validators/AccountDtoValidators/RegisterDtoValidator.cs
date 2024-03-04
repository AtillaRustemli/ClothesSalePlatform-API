using ClothesSalePlatform.DTOs.AccountDTOs;
using FluentValidation;
using System.Data;

namespace ClothesSalePlatform.Validators.AccountDtoValidators
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r=>r.FullName)
                .NotEmpty().WithMessage("Bosh Saxlamaq olmaz")
                .MaximumLength(50).WithMessage("Chox uzundur");
            RuleFor(r=>r.UserName)
                .NotEmpty().WithMessage("Bosh Saxlamaq olmaz")
                .MaximumLength(50).WithMessage("Chox uzundur");
            RuleFor(r=>r.Email)
                .NotEmpty().WithMessage("Bosh Saxlamaq olmaz")
                .MaximumLength(100).WithMessage("Simvol sayi 100-den chox olmamalidir!")
                .EmailAddress().WithMessage("Email olmalidir");
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Bosh Saxlamaq olmaz")
                .MinimumLength(8).WithMessage("Simvol sayi 8-den yuxari olmalidir!");


            RuleFor(r => r.RepeatPassword)
                .NotEmpty().WithMessage("Bosh Saxlamaq olmaz")
                .MinimumLength(8).WithMessage("Simvol sayi 8-den yuxari olmalidir!");
                
            RuleFor(r => r).Custom((r, custom) =>
            {
                if (r.RepeatPassword != r.Password)
                {
                    custom.AddFailure("RepeatPassword", "Passworda beraber olmalidir!");
                }
            });

        }
    }
}
