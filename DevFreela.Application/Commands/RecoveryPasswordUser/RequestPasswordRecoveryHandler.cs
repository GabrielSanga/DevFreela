using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Notifications;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.Application.Commands.RecoveryPasswordUser
{
    public class RequestPasswordRecoveryHandler : IRequestHandler<RequestPasswordRecoveryCommand, ResultViewModel>
    {
        public readonly IEmailService _emailService;
        public readonly IUserRepository _userRepository;
        public readonly IMemoryCache _memoryCache;

        public RequestPasswordRecoveryHandler(IEmailService emailService, IUserRepository userRepository, IMemoryCache memoryCache) 
        { 
            _emailService = emailService;
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }

        public async Task<ResultViewModel> Handle(RequestPasswordRecoveryCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email);

            if (user == null) { 
                return ResultViewModel.Fail("Email não encontrado");
            }

            var code = new Random().Next(100000, 999999).ToString();    

            var cacheKey = $"RecoveryPasswordCode:{user.Email}";

            _memoryCache.Set(cacheKey, code, TimeSpan.FromMinutes(30));

            await _emailService.SendAsync(user.Email, "Código de recuperação de senha", $"Seu código de recuperação de senha é: {code}. Ele é válido por 30 minutos.");

            return ResultViewModel.Success();
        }
    }
}
