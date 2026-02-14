using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.RecoveryPasswordUser
{
    public class ValidateRecoveryCodeCommand : IRequest<ResultViewModel>
    {

        public string Email { get; set; }

        public string Code { get; set; }
    }
}
