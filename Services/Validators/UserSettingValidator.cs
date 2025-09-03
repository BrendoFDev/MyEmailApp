using FluentValidation;
using MyEmailApp.Entities;

namespace MyEmailApp.Services.Validators
{
    public class UserSettingValidator : AbstractValidator<UserSettings>
    {
        public UserSettingValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email não é válido!");
            RuleFor(x => x.AppPassword).NotEmpty().WithMessage("Senha não pode ser vazia!");
        }
    }
}
