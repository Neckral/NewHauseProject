using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using BuisnesLogicLayer.DTO;

namespace BlazorFront.Validation
{
    public class AdEditValidator : AbstractValidator<AdEditDTO>
    {
        public AdEditValidator()
        {
            RuleFor(a => a.Price).NotEmpty().WithMessage("Поле не може бути пустим")
                            .GreaterThan(-1).WithMessage("Поле не має бути від'ємним числом");

            RuleFor(a => a.AreaOfHouse).NotEmpty().WithMessage("Поле не може бути пустим")
                           .GreaterThan(-1).WithMessage("Поле не має бути від'ємним числом");

            RuleFor(a => a.FloorAmount).NotEmpty().WithMessage("Поле не може бути пустим")
                           .GreaterThan(-1).WithMessage("Поле не має бути від'ємним числом");

            RuleFor(a => a.RoomNumber).NotEmpty().WithMessage("Поле не може бути пустим")
                           .GreaterThan(-1).WithMessage("Поле не має бути від'ємним числом");

            RuleFor(a => a.HouseYear).NotEmpty().WithMessage("Поле не може бути пустим");

            RuleFor(a => a.Region).NotEmpty().WithMessage("Поле не може бути пустим");

            RuleFor(a => a.District).NotEmpty().WithMessage("Поле не може бути пустим");

            RuleFor(a => a.City).NotEmpty().WithMessage("Поле не може бути пустим");

            RuleFor(a => a.Street).NotEmpty().WithMessage("Поле не може бути пустим");

            RuleFor(a => a.HouseNumber).NotEmpty().WithMessage("Поле не може бути пустим");

            RuleFor(a => a.HouseType).NotEmpty().WithMessage("Поле не може бути пустим");
        }
    }
}
