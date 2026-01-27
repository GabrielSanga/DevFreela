using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {         
        private readonly IMediator _mediator;
        private readonly IProjectRepository _repository;

        public InsertProjectHandler(IMediator mediator, IProjectRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();

            await _repository.Insert(project);

            var notification = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
            await _mediator.Publish(notification);

            return ResultViewModel<int>.Success(project.Id);
        }
    }
}
