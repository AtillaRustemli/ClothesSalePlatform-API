using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.PaymentDTOs;
using FluentValidation;

namespace ClothesSalePlatform.Validators.PaymentDtoValidators
{
    public class ProducInfoDtoValidator:AbstractValidator<List<ProducInfoDto>>
    {
        private readonly AppDbContext _context;

        public ProducInfoDtoValidator(AppDbContext context)
        {
            _context = context;
            RuleFor(p=>p)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!")
                .Custom((pi, custom) =>
                {
                    foreach (var item in pi)
                    {
                      if (_context.Products.Where(p => !p.IsDeleted).FirstOrDefault(p => p.Id == item.ProductId) == null) custom.AddFailure("ProductId", "Bele mehsul yoxdur");
                        if (item.Quantity <= 0) custom.AddFailure("Quantity", "mehsul sayi 0-dan yuxari olmalidir!! yoxdur");
                    }
                   
                });
            //RuleFor(p=>p.Quantity)
            //    .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!")
            //    .Custom((pi, custom) =>
            //    {
            //        if (pi<=0) custom.AddFailure("Quantity", "mehsul sayi 0-dan yuxari olmalidir!! yoxdur");
            //    });
        }
    }
}
