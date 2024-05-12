using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class UpdateUserStatusCommand : IRequest<int>
    {
        public int Id { get; set; } 
        public int IsActive { get; set; }
    }
}
