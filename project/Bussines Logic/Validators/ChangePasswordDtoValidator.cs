using FluentValidation;
using API.DTOs;

namespace Bussines_Logic.Validators
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x.NewPassword).MinimumLength(6).WithMessage("Пароль має містити мінімум 6 символів.");
        }
    }
} 