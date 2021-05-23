using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using BuisnesLogicLayer.DTO;

namespace BuisnesLogicLayer.Validation
{
    public class UserValidator : AbstractValidator<UserRegisterDTO>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            RuleFor(u => u.PasswordHash)
                .NotEmpty()
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,30}$");

            RuleFor(u => u.PasswordConfirmation)
                .NotEmpty()
                .Equal(u => u.PasswordHash);

            RuleFor(u => u.Name)
                .NotEmpty()
                .Matches(@"^\S+");

            RuleFor(u => u.Surname)
                .NotEmpty()
                .Matches(@"^\S+");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\d{9}");
        }
    }
}
