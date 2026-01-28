using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertComment
{
    public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repositoryProject;
        private readonly IUserRepository _userRepository;

        public InsertCommentHandler(IProjectRepository repositoryProject, IUserRepository userRepository)
        {
            _repositoryProject = repositoryProject;
            _userRepository = userRepository;
        }

        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var project = await _repositoryProject.GetById(request.IdProject);
            var user = await _userRepository.GetById(request.IdUser);

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
