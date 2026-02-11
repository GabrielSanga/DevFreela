using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Authenticate;
using MediatR;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, ResultViewModel<LoginViewModel>>
    {

        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;

        public LoginUserHandler(IUserRepository userRepository, IAuthService authService)
        {
            _repository = userRepository;
            _authService = authService;
        }

        public async Task<ResultViewModel<LoginViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var hash = _authService.ComputeHash(request.Password);

            var user = await _repository.GetByEmailAndPassword(request.Email, hash);

            if (user == null)
            {
                return ResultViewModel<LoginViewModel>.Fail("Credenciais inválidas.");
            }

            var token = _authService.GenerateToken(user.Email, user.Role);

            return ResultViewModel<LoginViewModel>.Success(new LoginViewModel(token));
        }
    }
}
