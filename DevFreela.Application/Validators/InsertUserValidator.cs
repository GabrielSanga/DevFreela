using DevFreela.Application.Commands.InsertUser;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class InsertUserValidator : AbstractValidator<InsertUserCommand>
    {
        public InsertUserValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                    .WithMessage("E-mail não pode ser nulo!")
                .EmailAddress()
                    .WithMessage("E-mail inválido!");

            RuleFor(u => u.BirthDate)
                .Must(d => d <= DateTime.Now.AddYears(-18))
                    .WithMessage("Usuário deve ser maior de 18 anos.");
        }

    }
}
