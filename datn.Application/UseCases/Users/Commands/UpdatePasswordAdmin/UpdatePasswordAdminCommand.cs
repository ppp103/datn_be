using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class UpdatePasswordAdminCommand : IRequest<UpdatePasswordResponse>
    {
        public int Id { get; set; }
        public string NewPassword { get; set; }
    }
}
