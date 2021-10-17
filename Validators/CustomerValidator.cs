using EcoLease_API.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 20).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid")
                .Must(BeAValidName).WithMessage("{PropertyName} Contains Invalid Charachters");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 20).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid")
                .Must(BeAValidName).WithMessage("{PropertyName} Contains Invalid Charachters");

            RuleFor(c => c.DateOfBirth)
                .Must(BeAValidAge).WithMessage("{PropertyName} Contains Invalid Charachters");

            RuleFor(c => c.Email).EmailAddress().WithMessage("{PropertyName} is Invalid");

            RuleFor(c => c.PhoneNo)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(5, 15).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid");

        }

        private bool BeAValidAge(DateTime age)
        {
            //returns true if the input year is not in the future and not older than 120 years
            return age <= DateTime.Now && age.Year > (DateTime.Now.Year - 120);
        }

        private bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
        }



    }
}
