using FluentValidation;
using MyEmailApp.Entities;

namespace MyEmailApp.Services.Validators
{
    public class EmailModelValidator : AbstractValidator<EmailModel>
    {
        public EmailModelValidator()
        {
            RuleFor(x => x.ModelName)
                .NotEmpty().WithMessage("Nome do modelo não pode ser nulo");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Assunto do email não pode ser nulo");
        }
    }
}
