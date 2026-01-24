using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertUserSkill
{
    public class InsertUserSkillHandler : IRequestHandler<InsertUserSkillCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;

        public InsertUserSkillHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultViewModel> Handle(InsertUserSkillCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.Id);

            if (user is null)
            {
                return ResultViewModel.Fail("Usuário não encontrado");
            }

            var userSkills = request.SkillsIds.Select(s => new UserSkill(request.Id, s)).ToList();

            await _dbContext.UserSkills.AddRangeAsync(userSkills);
            await _dbContext.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
