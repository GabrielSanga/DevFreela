using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetById(int id)
        {
            var user = await _dbContext.Users
                                .Include(u => u.Skills)
                                .ThenInclude(s => s.Skill)
                                .SingleOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<int> Insert(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task InsertSkill(List<UserSkill> userSkills)
        {
            await _dbContext.UserSkills.AddRangeAsync(userSkills);
            await _dbContext.SaveChangesAsync();
        }
    }
}
