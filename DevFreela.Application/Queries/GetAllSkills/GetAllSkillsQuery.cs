using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQuery : IRequest<ResultViewModel<List<SkillViewModel>>>
    {
    }
}
