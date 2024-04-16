using datn.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class CreateTestCommand : IRequest<TestDto>
    {
        public string TestName { get; set; }

        public int Time { get; set; }

        public int TotalPoint {  get; set; }    

        public int NumberOfQuestion { get; set; }

        public List<int> Ids { get; set; }
    }
}
