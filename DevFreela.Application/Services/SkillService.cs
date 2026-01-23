using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DevFreela.Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _dbContext;

        public SkillService(DevFreelaDbContext dbContent)
        {
            _dbContext = dbContent;
        }

        public ResultViewModel<List<SkillViewModel>> GetAll()
        {
            var skills = _dbContext.Skills.Where(s => !s.IsDeleted).ToList();

            var model = skills.Select(s => SkillViewModel.FromEntity(s)).ToList();

            return ResultViewModel<List<SkillViewModel>>.Success(model);
        }

        public ResultViewModel<SkillViewModel> Insert(CreateSkillInputModel inputModel)
        {
            var skill = inputModel.ToEntity();

            _dbContext.Skills.Add(skill);
            _dbContext.SaveChanges();

            return ResultViewModel<SkillViewModel>.Success(SkillViewModel.FromEntity(skill));
        }
    }
}
