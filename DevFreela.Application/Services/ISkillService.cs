using DevFreela.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Services
{
    public interface ISkillService
    {
        ResultViewModel<List<SkillViewModel>> GetAll();
        ResultViewModel<SkillViewModel> Insert(CreateSkillInputModel model);
    }
}
