using EcoLease_API.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Validators
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {
            RuleFor(r => r.LeaseBegin)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidStartDate).WithMessage("{PropertyName} is not correct!");

            RuleFor(r => r.LeaseLast)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidEndDate).WithMessage("{PropertyName} is not correct!");

            RuleFor(v => v.Status).NotEmpty().WithMessage("{PropertyName} is Empty");

        }
        protected bool BeAValidStartDate(DateTime date)
        {
            int currentYear = DateTime.Now.Year;

            //returns true if the input year is maximal a year ahead and not older than 6 years
            return date.Year <= (currentYear + 1) && date.Year >= (currentYear - 6);
        }

        protected bool BeAValidEndDate(DateTime date)
        {
            int currentYear = DateTime.Now.Year;

            //returns true if the input year is not older than 6 years(because of update) and not in the future with 6 years(5 years max)
            return date.Year <= (currentYear + 6) && date.Year >= (currentYear - 6);
        }
    }
}
