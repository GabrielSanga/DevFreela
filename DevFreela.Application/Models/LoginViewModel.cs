using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Models
{
    public class LoginViewModel
    {
        public LoginViewModel(string token)
        {
            Token = token;
        }

        public string Token { get; set; }

    }
}
