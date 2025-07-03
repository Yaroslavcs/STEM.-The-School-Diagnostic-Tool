using FluentValidation;
using ProjectApi.API.DTOs;

namespace ProjectApi.Bussines_Logic.Validators
{
    public class PagedQueryDtoValidator : AbstractValidator<PagedQueryDto>
    {
        public PagedQueryDtoValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
} 