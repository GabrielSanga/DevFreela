using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Commands.InsertUserSkill
{
    public class InsertUserSkillCommand : IRequest<ResultViewModel>
    {
        public int[] SkillsIds { get; set; }
        public int Id { get; set; }
    }
}
