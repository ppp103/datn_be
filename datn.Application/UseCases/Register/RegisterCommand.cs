﻿using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class RegisterCommand : IRequest<RegistrationResponse>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; } 

        public string Email { get; set; }

        public int Role { get; set; }
    }
}
