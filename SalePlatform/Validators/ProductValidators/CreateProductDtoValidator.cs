﻿using ClothesSalePlatform.DTOs.ProductDTOs;
using FluentValidation;

namespace ClothesSalePlatform.Validators.ProductValidators
{
    public class CreateProductDtoValidator:AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p => p.Color)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p => p.Price)
                //.Must(BeInteger).WithMessage("Eded Olmalidir")
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!")
                .Must(p => p >= 0).WithMessage("Mehsulun qiymeti 0-dan ashagi ola bilmez!!!");
            RuleFor(p => p.ProductCount)
                .Must(p => p >= 0).WithMessage("Mehsul sayi 0-dan ashagi ola bilmez!!!");
            //    .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");-->ProductCount 0-a beraber ola biler!
            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p => p.SizeId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p => p.StoreId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p => p.BrandId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            RuleFor(p => p.GenderId)
                .NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            //RuleForEach(p => new string[]{ p.Name, p.Color})
            //.NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            //RuleForEach(p => new int[]{ p.ProductCount, p.CategoryId, p.SizeId, p.StoreId, p.BrandId, p.GenderId })
            //.NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
            //RuleFor(p=> p.Price)
            //.NotEmpty().WithMessage("Bosh saxlamaq olmaz!!");
        }

      
    }
}
