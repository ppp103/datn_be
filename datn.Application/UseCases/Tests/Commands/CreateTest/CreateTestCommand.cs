using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class CreateTestCommand : IRequest<Test>
    {
        public string? TestName { get; set; }

        public int? Time { get; set; }

        public List<int> Id { get; set; }
    }
}
