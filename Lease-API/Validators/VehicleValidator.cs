using EcoLease_API.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Validators
{
    public class VehicleValidator : AbstractValidator<Vehicle>
    {
        public VehicleValidator()
        {
            RuleFor(v => v.Make)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 20).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid")
                .Must(BeAValidMake).WithMessage("{PropertyName} Contains Invalid Charachters");

            RuleFor(v => v.Model)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 20).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid");

            RuleFor(v => v.Registered)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidYear).WithMessage("{PropertyName} is Invalid");

            RuleFor(v => v.PlateNo)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 10).WithMessage("Length ({TotalLength}) of {PropertyName} is Invalid");

            RuleFor(v => v.Km)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .InclusiveBetween(10, 200000).WithMessage("{PropertyName} is Invalid");

            RuleFor(v => v.Img)
                .NotEmpty().WithMessage("{PropertyName} is Empty");

            RuleFor(v => v.Price)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .InclusiveBetween(200, 1000).WithMessage("{PropertyName} is Invalid");

            RuleFor(v => v.Status).NotEmpty().WithMessage("{PropertyName} is Empty");
        }

        private bool BeAValidMake(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
        }

        protected bool BeAValidYear(int year)
        {
            //current year in string
            int currentYear = DateTime.Now.Year;

            //returns true if the input year is not in the future and not older than 20 years
            return year <= currentYear && year > (currentYear - 20);
        }
    }
}
