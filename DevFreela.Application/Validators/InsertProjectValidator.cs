using DevFreela.Application.Commands.InsertProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class InsertProjectValidator : AbstractValidator<InsertProjectCommand>
    {
        public InsertProjectValidator()
        {
            RuleFor(P => P.Title)
                .NotEmpty()
                    .WithMessage("Título não pode ser vazio.")
                .MaximumLength(50)
                    .WithMessage("Título deve ter no máximo 50 caracteres.");

            RuleFor(P => P.TotalCost)
                .GreaterThanOrEqualTo(10)
                    .WithMessage("Custo total deve ser maior ou igual a 10 reais.");
        }

    }
}
