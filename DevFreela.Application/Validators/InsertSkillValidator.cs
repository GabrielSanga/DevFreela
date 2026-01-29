using DevFreela.Application.Commands.InserSkill;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Validators
{
    public class InsertSkillValidator : AbstractValidator<InsertSkillCommand>
    {

        public InsertSkillValidator()
        {
            RuleFor(s => s.Description)
                .NotEmpty()
                    .WithMessage("Nome da skill não pode ser vazio.")
                .MaximumLength(50)
                    .WithMessage("Nome da skill deve ter no máximo 50 caracteres.");
        }   

    }
}
