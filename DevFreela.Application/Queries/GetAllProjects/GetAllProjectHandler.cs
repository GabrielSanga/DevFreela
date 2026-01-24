using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectHandler : IRequestHandler<GetAllProjectQuery, ResultViewModel<List<ProjectItemViewModel>>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetAllProjectHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            var projects = await _dbContext.Projects
                                    .Include(p => p.Client)
                                    .Include(p => p.Freelancer)
                                    .Skip(request.Page * request.Size)
                                    .Take(request.Size)
                                    .Where(p => !p.IsDeleted && (request.QueryString == "" || p.Title.Contains(request.QueryString) || p.Description.Contains(request.QueryString)))
                                    .ToListAsync();

            var model = projects.Select(p => ProjectItemViewModel.FromEntity(p)).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }
    }
}
