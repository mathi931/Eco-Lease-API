using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Validators
{
    public class FileValidator : AbstractValidator<string>
    {
        public FileValidator()
        {
            RuleFor(fileName => fileName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidFileName).WithMessage("{PropertyName} is Invalid")
                .MinimumLength(8).WithMessage("{PropertyName} length is Invalid!");
        }

        private bool BeAValidFileName(string fileName)
        {
            return fileName.Contains(".jpg") || fileName.Contains(".pdf");
        }
    }
}
