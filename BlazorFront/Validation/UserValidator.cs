using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using BuisnesLogicLayer.DTO;

namespace BlazorFront.Validation
{
    public class UserValidator : AbstractValidator<UserRegisterDTO>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Поле не має бути пустим")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Поле Почта заповнено неправильно");

            RuleFor(u => u.PasswordHash)
                .NotEmpty().WithMessage("Поле не має бути пустим")
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,30}$").WithMessage("Пароль не надійний");

            RuleFor(u => u.PasswordConfirmation)
                .NotEmpty().WithMessage("Поле не має бути пустим")
                .Equal(u => u.PasswordHash).WithMessage("Паролі не співпадають");

            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Поле не має бути пустим")
                .Matches(@"^\S+").WithMessage("Поле ім'я заповнено неправильно");

            RuleFor(u => u.Surname)
                .NotEmpty().WithMessage("Поле не має бути пустим")
                .Matches(@"^\S+").WithMessage("Поле Прізвище заповнено неправильно");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithMessage("Поле не має бути пустим")
                .Matches(@"^\d{9}").WithMessage("Поле Телефон заповнено неправильно");
        }
    }
}
