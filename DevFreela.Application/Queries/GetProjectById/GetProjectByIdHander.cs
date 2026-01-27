using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdHander : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetProjectByIdHander(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects
                            .Include(p => p.Client)
                            .Include(p => p.Freelancer)
                            .Include(p => p.Comments)
                            .SingleOrDefaultAsync(p => p.Id == request.Id);

            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Fail("Projeto não encontrado");
            }

            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }
    }
}
