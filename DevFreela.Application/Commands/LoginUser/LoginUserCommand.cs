using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<ResultViewModel<LoginViewModel>>
    {
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
