using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Authenticate;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.Application.Commands.RecoveryPasswordUser
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, ResultViewModel>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public ChangePasswordHandler(IMemoryCache memoryCache, IUserRepository userRepository, IAuthService authService) { 
            _memoryCache = memoryCache;
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<ResultViewModel> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var cacheKey = $"RecoveryPasswordCode:{request.Email}";

            if (!_memoryCache.TryGetValue(cacheKey, out string? cachedCode) || cachedCode != request.Code)
            {
                return ResultViewModel.Fail("Código de recuperação não encontrado ou expirado.");
            }

            _memoryCache.Remove(cacheKey);

            var user = await _userRepository.GetByEmail(request.Email);

            if (user == null)
            {
                return ResultViewModel.Fail("Email não encontrado.");
            }

            user.UpdatePassword(_authService.ComputeHash(request.NewPassword));
            await _userRepository.Update(user);

            return ResultViewModel.Success();
        }
    }
}
