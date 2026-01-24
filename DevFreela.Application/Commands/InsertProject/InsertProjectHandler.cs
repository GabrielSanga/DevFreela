using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Commands.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<ProjectViewModel>>
    {         
        private readonly DevFreelaDbContext _dbContext;

        public InsertProjectHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultViewModel<ProjectViewModel>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();

            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

            return ResultViewModel<ProjectViewModel>.Success(ProjectViewModel.FromEntity(project));
        }
    }
}
