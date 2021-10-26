using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Validators
{
    public class IDValidator : AbstractValidator<int>
    {
        public IDValidator()
        {
            RuleFor(id => id).Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("{PropertyName} length is not correct!")
            .LessThan(9999).WithMessage("{PropertyName} length is not correct!");

        }
    }
}
