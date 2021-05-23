using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using BuisnesLogicLayer.DTO;

namespace BuisnesLogicLayer.Validation
{
    public class AdValidator : AbstractValidator<AdCreateDTO>
    {
        public AdValidator()
        {
            RuleFor(a => a.Price).NotEmpty()
                           .GreaterThan(-1);

            RuleFor(a => a.AreaOfHouse).NotEmpty()
                           .GreaterThan(-1);

            RuleFor(a => a.FloorAmount).NotEmpty()
                           .GreaterThan(-1);

            RuleFor(a => a.RoomNumber).NotEmpty()
                           .GreaterThan(-1);

            RuleFor(a => a.HouseYear).NotEmpty();

            RuleFor(a => a.Region).NotEmpty();

            RuleFor(a => a.District).NotEmpty();

            RuleFor(a => a.City).NotEmpty();

            RuleFor(a => a.Street).NotEmpty();

            RuleFor(a => a.HouseNumber).NotEmpty();

            RuleFor(a => a.HouseType).NotEmpty();

        }
    }
}
