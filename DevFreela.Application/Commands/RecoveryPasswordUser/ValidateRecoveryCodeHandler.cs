using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.Application.Commands.RecoveryPasswordUser
{
    public class ValidateRecoveryCodeHandler : IRequestHandler<ValidateRecoveryCodeCommand, ResultViewModel>
    {
        private readonly IMemoryCache _memoryCache;

        public ValidateRecoveryCodeHandler(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        public async Task<ResultViewModel> Handle(ValidateRecoveryCodeCommand request, CancellationToken cancellationToken)
        {
            var cacheKey = $"RecoveryPasswordCode:{request.Email}";

            if (!_memoryCache.TryGetValue(cacheKey, out string? cachedCode) || cachedCode != request.Code)
            {
                return ResultViewModel.Fail("Código de recuperação não encontrado ou expirado.");
            }

            return ResultViewModel.Success();
        }
    }
}
