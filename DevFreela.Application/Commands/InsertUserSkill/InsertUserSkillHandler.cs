using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertUserSkill
{
    public class InsertUserSkillHandler : IRequestHandler<InsertUserSkillCommand, ResultViewModel>
    {
        private readonly IUserRepository _repository;

        public InsertUserSkillHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(InsertUserSkillCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.Id);   

            if (user is null)
            {
                return ResultViewModel.Fail("Usuário não encontrado");
            }

            var userSkills = request.SkillsIds.Select(s => new UserSkill(request.Id, s)).ToList();

            await _repository.InsertSkill(userSkills);

            return ResultViewModel.Success();
        }
    }
}
