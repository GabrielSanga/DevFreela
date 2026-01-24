using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, ResultViewModel<List<SkillViewModel>>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetAllSkillsHandler(DevFreelaDbContext dbContent)
        {
            _dbContext = dbContent;
        }

        public async Task<ResultViewModel<List<SkillViewModel>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await _dbContext.Skills.Where(s => !s.IsDeleted).ToListAsync();

            var model = skills.Select(s => SkillViewModel.FromEntity(s)).ToList();

            return ResultViewModel<List<SkillViewModel>>.Success(model);
        }
    }
}
