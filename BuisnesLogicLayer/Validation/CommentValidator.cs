using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using BuisnesLogicLayer.DTO;

namespace BuisnesLogicLayer.Validation
{
    public class CommentValidator : AbstractValidator<CommentCreateDTO>
    {
        public CommentValidator()
        {
            RuleFor(c => c.Text).NotEmpty();
        }
    }
}
