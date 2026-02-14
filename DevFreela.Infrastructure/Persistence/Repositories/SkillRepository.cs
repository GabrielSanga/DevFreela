using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        public readonly DevFreelaDbContext _dbContext;

        public SkillRepository(DevFreelaDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<List<Skill>> GetAll()
        {
            return await _dbContext.Skills.Where(s => !s.IsDeleted).ToListAsync();
        }

        public async Task<Skill?> GetById(int id)
        {
            return await _dbContext.Skills.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> Insert(Skill skill)
        {
            await _dbContext.Skills.AddAsync(skill);
            await _dbContext.SaveChangesAsync();

            return skill.Id;
        }
    }
}
