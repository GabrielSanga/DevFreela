using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Commands.InsertComment
{
    public class InsertCommentCommand : IRequest<ResultViewModel>
    {

        public string Content { get; set; }

        public int IdProject { get; set; }

        public int IdUser { get; set; }

        public ProjectComment ToEntity()
        {
            return new(Content, IdProject, IdUser);
        }
    }
}
