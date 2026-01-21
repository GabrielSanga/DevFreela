using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevFreela.Application.Services
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResultViewModel<UserViewModel> GetById(int id)
        {
            var user = _dbContext.Users
                .Include(u => u.Skills)
                    .ThenInclude(s => s.Skill)
                .SingleOrDefault(u => u.Id == id);

            if (user is null)
            {
                return ResultViewModel<UserViewModel>.Fail("Usuário não encontrado");
            }

            var model = UserViewModel.FromEntity(user);

            return ResultViewModel<UserViewModel>.Success(model);
        }

        public ResultViewModel<UserViewModel> Insert(CreateUserInputModel inputModel)
        {
            var user = inputModel.ToEntity();

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return ResultViewModel<UserViewModel>.Success(UserViewModel.FromEntity(user));  
        }

        public ResultViewModel InsertSkills(UserSkillsInputModel inputModel)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == inputModel.Id);

            if (user is null)
            {
                return ResultViewModel.Fail("Usuário não encontrado");
            }

            var userSkills = inputModel.SkillsIds.Select(s => new UserSkill(inputModel.Id, s)).ToList();

            _dbContext.UserSkills.AddRange(userSkills);
            _dbContext.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}
