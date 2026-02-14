using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.RecoveryPasswordUser
{
    public class RequestPasswordRecoveryCommand : IRequest<ResultViewModel>
    {
        public string Email { get;  set; }

    }
}
