using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Commands.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {         
        private readonly DevFreelaDbContext _dbContext;
        private readonly IMediator _mediator;

        public InsertProjectHandler(DevFreelaDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();

            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

            var notification = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
            await _mediator.Publish(notification);

            return ResultViewModel<int>.Success(project.Id);
        }
    }
}
