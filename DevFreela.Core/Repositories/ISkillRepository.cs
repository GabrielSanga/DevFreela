using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Core.Repositories
{
    public interface ISkillRepository
    {

        Task<Skill?> GetById(int id);

        Task<List<Skill>> GetAll();

        Task<int> Insert(Skill skill);

    }
}
