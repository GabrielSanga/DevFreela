using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;              

namespace DevFreela.Application.Commands.InsertComment
{
    public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IProjectRepository _repositoryProject;

        public InsertCommentHandler(DevFreelaDbContext dbContext, IProjectRepository repositoryProject)
        {
            _dbContext = dbContext;
            _repositoryProject = repositoryProject;
        }

        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var project = await _repositoryProject.GetById(request.IdProject);
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.IdUser);

            if (project is null)
            {
                return ResultViewModel.Fail("Projeto não encontrado");
            }

            if (user is null)
            {
                return ResultViewModel.Fail("Usuário não encontrado");
            }

            var comment = request.ToEntity();

            await _repositoryProject.AddComment(comment);

            return ResultViewModel.Success();
        }

    }
}
