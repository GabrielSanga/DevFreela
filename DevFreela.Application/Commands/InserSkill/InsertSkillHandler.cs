using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InserSkill
{
    public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public InsertSkillHandler(DevFreelaDbContext dbContent)
        {
            _dbContext = dbContent;
        }

        public async Task<ResultViewModel<int>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = request.ToEntity();

            await _dbContext.Skills.AddAsync(skill);
            await _dbContext.SaveChangesAsync();

            return ResultViewModel<int>.Success(skill.Id);
        }
    }
}
