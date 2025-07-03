using FluentValidation;
using ProjectApi.API.DTOs;

namespace ProjectApi.Bussines_Logic.Validators
{
    public class SchoolDtoValidator : AbstractValidator<SchoolDto>
    {
        public SchoolDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
        }
    }
} 